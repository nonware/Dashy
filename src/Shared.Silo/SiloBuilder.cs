using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;
using Serilog;
using Shared.Silo.Configuration;
using System.Net;
using static Shared.Silo.Configuration.PortConstants;
namespace Shared.Silo;
public interface ISiloBuilder { }
public static class SiloBuilder
{
    #region Private
    private static int _portOffset;
    #endregion

    #region Public
    public static void SetConsoleTitle(string project)
    {
        Console.Title = $"{project} on Gateway: {GatewayPort}, Silo: {FirstSiloPort + _portOffset}, Kestrel: {FirstKestrelPort + _portOffset}";
    }
    public static WebApplication BuildSilo<T>(string[] args, Action<WebApplicationBuilder, T, RuntimeConfig>? webAppBuilder = null) where T : SiloConfig
    {
        var host = WebApplication.CreateBuilder(args);
        
        host.Host.UseSerilog();
        
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

        #region Cluster Port Setup
        _portOffset = 0; //TODO Pull in from args as apart of script silo runner

        var config = host.Configuration.GetSection(nameof(SiloConfig)).Get<T>()!;
        var ports = new RuntimeConfig
        {
            GatewayPort = config.RuntimeConfig?.GatewayPort ?? GatewayPort,
            SiloPort = config.RuntimeConfig?.SiloPort ?? FirstSiloPort + _portOffset,
            KestrelHttpPort = config.RuntimeConfig?.KestrelHttpPort ?? FirstKestrelPort + _portOffset,
            KestrelHttpsPort = config.RuntimeConfig?.KestrelHttpsPort ?? FirstKestrelPort + MaxInstances + _portOffset
        };

        host.WebHost.UseKestrel(kestrelOptions =>
        {
            kestrelOptions.ListenAnyIP(ports.KestrelHttpPort.Value);
            kestrelOptions.ListenAnyIP(ports.KestrelHttpsPort.Value, options =>
            {
                options.UseHttps();
                options.UseConnectionLogging();
            });
        });
        #endregion

        host.Host.UseConsoleLifetime();

        host.Host.UseOrleans(silo =>
        {
            if (host.Environment.IsDevelopment())
            {
                // Makes logs noisy
                silo.AddActivityPropagation();
                // Liveness will cause the cluster to panic if hung a breakpoint. It is disabled in debug.
                silo.Configure<ClusterMembershipOptions>(cmo => cmo.LivenessEnabled = false);
            }

            silo.UseAdoNetClustering(options =>
            {
                options.Invariant = "System.Data.SqlClient";
                options.ConnectionString = config.ClusteringConnectionString;
            });
            
            var advertisingIP = IPAddress.Parse(host.Configuration["AdvertiseIP"] ?? IPAddress.Loopback.ToString());
            silo.ConfigureEndpoints(
                advertisingIP,
                ports.SiloPort.Value,
                ports.GatewayPort.Value,
                listenOnAnyHostAddress: true);

            silo.UseDashboard(options =>
            {
                options.HostSelf = false;
                options.Port = ports.KestrelHttpPort.Value;
                options.BasePath = nameof(silo);
            });

            silo.AddStartupTask(async (_, _) =>
            {
                SetConsoleTitle(host.Environment.ApplicationName);
            });
        });

        host.Services.AddServicesForSelfHostedDashboard();

        webAppBuilder?.Invoke(host, config, ports);

        var webApp = host.Build();
        webApp.UseOrleansDashboard(new OrleansDashboard.DashboardOptions { BasePath = "silo" });
        return webApp;
    }

    public static void OnExiting(string siloName)
    {
        Log.Information($"{siloName} has shutdown and exited from cluster.");
    }

    #endregion
}

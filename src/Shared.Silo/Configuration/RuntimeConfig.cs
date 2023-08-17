using Microsoft.Extensions.Logging;

namespace Shared.Silo.Configuration;

public class RuntimeConfig
{
    public int? GatewayPort { get; init; }
    public int? SiloPort { get; init; }
    public int? KestrelHttpPort { get; init; }
    public int? KestrelHttpsPort { get; init; }
}

public class SiloConfig
{
    public RuntimeConfig RuntimeConfig { get; init; } = new RuntimeConfig();
    public string ClusterId { get; init; } = string.Empty;
    public string ClusteringConnectionString { get; init; } = string.Empty;
    public string ServiceId { get; init; } = string.Empty;
    public LogLevel LogLevel { get; init; }
}

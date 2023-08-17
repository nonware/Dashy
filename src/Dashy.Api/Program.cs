using Shared.Silo;
using Shared.Silo.Configuration;

try
{
    await using var silo = SiloBuilder.BuildSilo<SiloConfig>(args, (builder, _, ports) =>
    {
        builder.Services.AddControllers();
        builder.Services.AddRouting();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddHttpsRedirection(options => options.HttpsPort = ports.KestrelHttpsPort);
    });

    silo.UseRouting();
    silo.UseEndpoints(options =>
    {
        options.MapControllers();
    });
    silo.UseSwagger();
    silo.UseSwaggerUI();

    await silo.RunAsync();
}
finally
{
    SiloBuilder.OnExiting("Api.Host");
}
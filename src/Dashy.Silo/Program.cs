using Shared.Silo;
using Shared.Silo.Configuration;

try
{
    await SiloBuilder.BuildSilo<SiloConfig>(args).RunAsync();
}
finally
{
    SiloBuilder.OnExiting("Grain.Silo");
}
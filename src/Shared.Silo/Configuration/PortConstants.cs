namespace Shared.Silo.Configuration;

internal class PortConstants
{
    internal const int BasePort = 40_000;
    internal const int MaxInstances = 100;
    internal const int GatewayPort = BasePort - 1;
    internal const int FirstSiloPort = BasePort;
    internal const int FirstKestrelPort = FirstSiloPort + MaxInstances;
}

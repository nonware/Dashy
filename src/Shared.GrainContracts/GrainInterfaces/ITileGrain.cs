namespace Shared.GrainContracts.GrainInterfaces
{
    public interface ITileGrain : IGrain
    {
        Task<TileState> GetStateAsync();
        Task SetStateAsync(TileState state);
    }
}

using Orleans.Runtime;
using Shared.GrainContracts;
using Shared.GrainContracts.GrainInterfaces;

namespace Grain.Host.Grains
{
    public class TileGrain : IGrainBase, ITileGrain
    {
        private readonly IPersistentState<TileState> _state;

        public TileGrain([PersistentState(nameof(TileState), "Tile")] IPersistentState<TileState> state)
        {
            _state = state;
        }

        public Task SetStateAsync(TileState tileState)
        {
            _state.State = tileState;
            return _state.WriteStateAsync();
        }

        public Task<TileState> GetStateAsync()
        {
            return Task.FromResult(_state.State);
        }

        public Task ClearStateAsync()
        {
            return Task.FromResult(_state.ClearStateAsync());
        }
        public IGrainContext GrainContext { get; }
    }
}

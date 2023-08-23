using Microsoft.AspNetCore.Mvc;
using Shared.GrainContracts.Base;
using Shared.Silo;

namespace Api.Host.Controllers.Base
{
    [ApiController]
    public abstract class ClusterAwareControllerBase : ControllerBase
    {
        protected IClusterClient ClusterClient { get; }
        protected ClusterAwareControllerBase(IClusterClient clusterClient)
        {
            ClusterClient = clusterClient;
        }

        public T CreateNewGrain<T>(GrainStateBase state) where T : IGrain
        {
            var grain = ClusterClient.GetNewGrain<T>(out var guidKey);
            state.Id = guidKey;
            return grain;
        }
    }
}

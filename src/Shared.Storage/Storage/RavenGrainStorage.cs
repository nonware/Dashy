using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime;
using Orleans.Storage;
using Raven.Client.Documents;
using Raven.Client.Exceptions;
using Serilog;

namespace Silo.Shared.Storage
{
    public class RavenGrainStorage : IGrainStorage
    {
        private readonly IDocumentStore _raven;
        private readonly ILogger<RavenGrainStorage> _logger;

        public RavenGrainStorage(IDocumentStore raven, ILogger<RavenGrainStorage> logger)
        {
            _raven = raven;
            _logger = logger;
        }
        public async Task ClearStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
        {

            try
            {
                using var session = _raven.OpenAsyncSession();
                var currentState = await session.LoadAsync<object>(grainId.Key.ToString());
                if (currentState == null)
                    return;

                session.Delete(currentState);
                await session.SaveChangesAsync();

                ResetGrainState(grainState);
            }
            catch (Exception e)
            {
                Log.Error("Error occurred clearing state {StateName} for Grain {GrainId}", e);
                throw;
            }
        }

        public async Task ReadStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
        {
            try
            {
                using var session = _raven.OpenAsyncSession();
                var currentState = await session.LoadAsync<T>(grainId.Key.ToString());
                if (currentState != null)
                {
                    grainState.RecordExists = true;
                    grainState.State = currentState;
                    grainState.ETag = session.Advanced.GetChangeVectorFor(currentState);
                }

                ResetGrainState(grainState);

                return;
            }
            catch (Exception e)
            {
                 Log.Error("Error occurred reading state {StateName} for Grain {GrainId}", e);
            }
        }

        public async Task WriteStateAsync<T>(string stateName, GrainId grainId, IGrainState<T> grainState)
        {
            try
            {
                using var session = _raven.OpenAsyncSession();
                await session.StoreAsync(grainState.State, grainId.Key.ToString());
                await session.SaveChangesAsync();

                ResetGrainState(grainState);
            }
            catch (ConcurrencyException ce)
            {
                 Log.Error("Concurrency error occurred writing state {StateName} for Grain {GrainId}", ce);
                throw new InconsistentStateException(ce.ActualChangeVector, ce.ExpectedChangeVector, ce);
            }
            catch (Exception e)
            {
                 Log.Error("Error occurred writing state {StateName} for Grain {GrainId}", e);
                throw;
            }
        }

        private static void ResetGrainState<T>(IGrainState<T> grainState)
        {
            grainState.RecordExists = false;
            grainState.State = Activator.CreateInstance<T>();
            grainState.ETag = default;
        }
    }
}
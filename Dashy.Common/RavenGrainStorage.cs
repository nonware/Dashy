using Orleans;
using Orleans.Runtime;
using Orleans.Storage;
using Raven.Client.Documents;
using Raven.Client.Exceptions;

namespace Dashy.Common
{
    public class RavenGrainStorage : IGrainStorage
    {
        private readonly IDocumentStore _raven;
        public RavenGrainStorage(IDocumentStore raven)
        {
            _raven = raven;
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
            catch(Exception ex)
            {
                //TODO Log
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
                //TODO Add log here when Serilog added
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
                //TODO Add log here when Serilog added
                throw new InconsistentStateException(ce.ActualChangeVector, ce.ExpectedChangeVector, ce);
            }
            catch (Exception e)
            {

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
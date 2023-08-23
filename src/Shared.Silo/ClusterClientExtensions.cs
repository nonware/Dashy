using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Silo
{
    public static class ClusterClientExtensions
    {
        public static GrainId AsGrainId<T>(this string key)
        {
            return GrainId.Create(typeof(T).Name, key);
        }

        public static T GetGrainWithId<T>(this IClusterClient clusterClient, string key) where T : IGrain
        {
            return clusterClient.GetGrain<T>(key.AsGrainId<T>());
        }

        public static T GetNewGrain<T>(this IClusterClient clusterClient, out string newKey) where T : IGrain
        {
            newKey = Guid.NewGuid().ToString();
            var newGrain = clusterClient.GetGrainWithId<T>(newKey);
            return newGrain;
        }
    }
}

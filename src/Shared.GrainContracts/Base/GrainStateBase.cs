using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GrainContracts.Base
{
    [GenerateSerializer]
    public abstract class GrainStateBase
    {
        //Puts a cap on class properties for a grain
        public const int MaxRecord = 100;
        [Id(MaxRecord)]
        public string? Id { get; set; }
    }
}

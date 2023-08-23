using Shared.GrainContracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.GrainContracts
{
    [GenerateSerializer]
    public class TileState : GrainStateBase
    {
        [Id(0)]
        public double CoordinateX { get; set;}
        [Id(1)]
        public double CoordinateY { get; set;}
        [Id(2)]
        public double DimensionX { get; set; }
        [Id(3)]
        public double DimensionY { get; set; }
    }
}

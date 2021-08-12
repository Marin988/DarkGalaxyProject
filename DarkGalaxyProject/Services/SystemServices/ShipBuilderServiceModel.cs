using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices
{
    public class ShipBuilderServiceModel
    {
        public string systemId { get; set; }

        public string ShipType { get; set; }

        public int PricePerShip { get; set; }

        public int BuildTime { get; set; }

        public DateTime? FinishedBuildingTime { get; set; }

        public int Count { get; set; }
    }
}

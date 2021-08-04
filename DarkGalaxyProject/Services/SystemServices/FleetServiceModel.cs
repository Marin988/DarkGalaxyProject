using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices
{
    public class FleetServiceModel
    {
        public List<ShipServiceModel> Ships { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public bool Outgoing { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.System
{
    public class FleetFormModel
    {
        public List<ShipViewModel> Ships { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public bool Outgoing { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.System
{
    public class FleetViewFormModel
    {
        public List<ShipViewModel> Ships { get; set; }

        public List<FleetFormModel> Fleets { get; set; }

        public int BattleShipCount { get; set; }

        public int TransportShipCount { get; set; }

        public int ColonizerCount { get; set; }

        public string HostSystemId { get; set; }

        public int HostSystemPosition { get; set; }

        public int? DestinationSystemPosition { get; set; }

        public string PlayerId { get; set; }
    }
}

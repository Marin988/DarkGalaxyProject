using DarkGalaxyProject.Services.SystemServices.Models;
using System.Collections.Generic;

namespace DarkGalaxyProject.Models.System
{
    public class FleetViewFormModel
    {
        public IEnumerable<ShipServiceModel> Ships { get; set; }

        public IEnumerable<FleetServiceModel> Fleets { get; set; }

        public int BattleShipCount { get; set; }

        public int TransportShipCount { get; set; }

        public int ColonizerCount { get; set; }

        public int? DestinationSystemPosition { get; set; }

        public int Cargo { get; set; }

        public HostSystemInfoServiceModel HostSystemInfo { get; set; }
    }
}

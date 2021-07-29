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

        public int GoliathCount { get; set; }

        public int VengeanceCount { get; set; }

        public int LeonovCount { get; set; }

        public string HostSystemId { get; set; }

        public int HostSystemPosition { get; set; }

        public int? DestinationSystemPosition { get; set; }

        public string PlayerId { get; set; }

        public bool Colonizer { get; set; }
    }
}

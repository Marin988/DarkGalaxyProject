using System;
using System.Collections.Generic;

namespace DarkGalaxyProject.Services.SystemServices.Models
{
    public class FleetServiceModel
    {
        public List<ShipServiceModel> Ships { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public bool Outgoing { get; set; }

        public int FuelPricePerSystemTravelled { get; set; }

        public int? DestinationSystemPosition { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices.Models
{
    public class ShipServiceModel
    {
        public string Type { get; set; }

        public int Damage { get; set; }

        public int HP { get; set; }

        public int MaxHP { get; set; }

        public int MaxStorage { get; set; }

        public int Storage { get; set; }

        public int Speed { get; set; }

        public bool OnMission { get; set; }

        public string DealId { get; set; }

        public string FleetId { get; set; }
    }
}

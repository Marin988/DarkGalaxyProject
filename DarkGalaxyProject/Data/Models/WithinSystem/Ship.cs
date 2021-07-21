using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Ship
    {
        public Ship(ShipType type, string systemId)
        {
            Type = type;
            SystemId = systemId;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ShipType Type { get; set; }

        public int Damage => (int)Type * 20;

        public int MaxHP => (int)Type * 20;

        public int HP { get; set; }

        public int MaxStorage => (int)Type * 20;

        public int Storage { get; set; }

        public int Speed => (int)Type * 20;

        
        public string SystemId { get; set; }

        public System System { get; set; }

        [Required]
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public string AllianceId { get; set; }
        public Alliance Alliance { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Factories
    {
        public string Id { get; set; }

        public int Level { get; set; }

        public int Income { get; set; }

        public int UpgradeCost { get; set; }

        [Required]
        public string PlanetId { get; set; }

        public Planet Planet { get; set; }
    }
}

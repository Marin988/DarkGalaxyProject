using DarkGalaxyProject.Data.Models.WithinSystem.Planets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Buildings
{
    public class LivingQuarters
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int UpgradeCost => Level * 100;

        public int Level { get; set; }

        public int PopulationCapacity => Level * 1000;

        [Required]
        public string PlanetId { get; set; }

        public PopulatedPlanet Planet { get; set; }
    }
}

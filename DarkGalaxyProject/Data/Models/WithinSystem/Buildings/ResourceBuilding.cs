using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Data.Models.WithinSystem.Planets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Buildings
{
    public class ResourceBuilding
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ResourceBuildingType Type { get; set; }

        public int Production => Level * 100;

        public int EnergyCost => Level * 10;

        public int UpgradeCost => Level * 1000;

        public int Level { get; set; }

        [Required]
        public string PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}

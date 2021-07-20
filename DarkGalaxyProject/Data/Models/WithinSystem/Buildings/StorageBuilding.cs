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
    public class StorageBuilding
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public StorageBuildingType Type { get; set; }

        public int ResourceCapacity => 10000 * Level;

        public int UpgradeCost => 1000 * Level;

        public int Level { get; set; }

        [Required]
        public string PlanetId { get; set; }
        public Planet Planet { get; set; }
    }
}

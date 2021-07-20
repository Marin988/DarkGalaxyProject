using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Planets
{
    public class ResourcePlanet : Planet
    {
        public string CrystalMineId { get; set; }
        public ResourceBuilding CrystalMine { get; set; }

        public string FuelGeneratorId { get; set; }
        public ResourceBuilding FuelGenerator { get; set; }

        public string TitaniumMineId { get; set; }
        public ResourceBuilding TitaniumMine { get; set; }

        public string CrystalStorageId { get; set; }
        public StorageBuilding CrystalStorage { get; set; }

        public string FuelStorageId { get; set; }
        public StorageBuilding FuelStorage { get; set; }

        public string TitaniumStorageId { get; set; }
        public StorageBuilding TitaniumStorage { get; set; }
    }
}

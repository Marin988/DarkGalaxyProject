using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class ResourcePlanetViewModel : PlanetViewModel
    {
        public ResourceBuildingViewModel CrystalMine { get; set; }

        public ResourceBuildingViewModel FuelGenerator { get; set; }

        public ResourceBuildingViewModel TitaniumMine { get; set; }

        public StorageBuildingViewModel CrystalStorage { get; set; }

        public StorageBuildingViewModel FuelStorage { get; set; }

        public StorageBuildingViewModel TitaniumStorage { get; set; }
    }
}

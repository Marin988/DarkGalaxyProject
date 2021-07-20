using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Planets
{
    public class EnergyPlanet : Planet
    {

        public string SolarPanelId { get; set; }
        public ResourceBuilding SolarPanel { get; set; }

        public string GeothermalPlantId { get; set; }
        public ResourceBuilding GeothermalPlant { get; set; }

        public string FuelToEnergyCenterId { get; set; }
        public ResourceBuilding FuelToEnergyCenter { get; set; }
    }
}

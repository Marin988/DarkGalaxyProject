namespace DarkGalaxyProject.Models
{
    public class EnergyPlanetViewModel : PlanetViewModel
    {
        public ResourceBuildingViewModel SolarPanel { get; set; }
        public ResourceBuildingViewModel GeothermalPlant { get; set; }
        public ResourceBuildingViewModel FuelToEnergyCenter { get; set; }
    }
}
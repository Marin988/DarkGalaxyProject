using DarkGalaxyProject.Services.PlanetServices.Models;

namespace DarkGalaxyProject.Services.PlanetServices
{
    public interface IPlanetService
    {
        public PlanetServiceModel Planet(string planetId);

        public string StartUpgrade(string buildingId, string planetId, string playerId);

        public string Terraform(string planetId, string playerId);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlanetServices
{
    public interface IPlanetService
    {
        public PlanetServiceModel Planet(string planetId);

        public bool LevelUp(string buildingId);

        public bool StartUpgrade(string buildingId, string planetId, string playerId);

        public bool SetUpgradeTime(string buildingId);

        public bool NullifyUpgradeTime(string buildingId);
        
    }
}

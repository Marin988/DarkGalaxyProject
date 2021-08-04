using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlanetServices
{
    public class PlanetService : IPlanetService
    {
        private readonly ApplicationDbContext data;

        public PlanetService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public bool LevelUp(string buildingId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.Level += 1;

            data.SaveChanges();

            return true;
        }

        public bool NullifyUpgradeTime(string buildingId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.UpgradeFinishTime = null;
            factory.UpgradeStartTime = null;

            data.SaveChanges();

            return true;
        }

        public PlanetServiceModel Planet(string planetId)
        {
            var systemId = data.Planets.First(p => p.Id == planetId).SystemId;

            var playerId = data.Systems.First(s => s.Id == systemId).PlayerId;

            var planet = data.Planets
                .Where(p => p.Id == planetId)
                .Select(p => new PlanetServiceModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Position = p.Position,
                    Type = p.Type.ToString(),
                    Factories = new FactoriesServiceModel
                    {
                        Id = p.Factories.Id,
                        Income = p.Factories.Income,
                        Level = p.Factories.Level,
                        UpgradeCost = p.Factories.UpgradeCost,
                        UpgradeFinishTime = p.Factories.UpgradeFinishTime,
                        UpgradeStartTime = p.Factories.UpgradeStartTime,
                        UpgradeTimeLength = p.Factories.UpgradeTimeLength
                    },
                    PlayerId = playerId
                })
                .First();

            return planet;
        }

        public bool SetUpgradeTime(string buildingId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.UpgradeFinishTime = DateTime.Now.AddSeconds(factory.UpgradeTimeLength);

            data.SaveChanges();

            return true;
        }

        public bool StartUpgrade(string buildingId, string planetId, string playerId)
        {
            var planet = data.Planets.First(p => p.Id == planetId);

            var systemId = planet.SystemId;

            var factory = data.Factories.First(f => f.Id == buildingId);

            var system = data.Systems.First(s => s.Id == systemId);

            var milkyCoin = data.Resources.First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == systemId);

            milkyCoin.Quantity -= factory.UpgradeCost;

            factory.UpgradeStartTime = DateTime.Now;

            if (system.PlayerId != playerId)
            {
                return false;
            }

            data.SaveChanges();

            return true;
        }
    }
}

using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Services.PlanetServices.Models;
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
                    Factories = p.Factories.Select(f => new FactorySeviceModel
                    {
                        Id = f.Id,
                        Income = f.Income,
                        Level = f.Level,
                        UpgradeCost = f.UpgradeCost,
                        UpgradeFinishTime = f.UpgradeFinishTime,
                        UpgradeTimeLength = f.UpgradeTimeLength,
                        Type = f.FactoryType.ToString()
                    }).ToList(),
                    PlayerId = playerId
                })
                .First();

            return planet;
        }

        public string StartUpgrade(string buildingId, string planetId, string playerId)
        {
            var planet = data.Planets.First(p => p.Id == planetId);

            var systemId = planet.SystemId;

            var factory = data.Factories.First(f => f.Id == buildingId);

            if(factory.UpgradeFinishTime != null)
            {
                return "This building is already in the process of upgrading";
            }

            var system = data.Systems.First(s => s.Id == systemId);

            var milkyCoin = data.Resources.First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == systemId);

            if (milkyCoin.Quantity < factory.UpgradeCost)
            {
                return $"You don't have enough {milkyCoin.Type.ToString()}.";
            }
            else
            {
                milkyCoin.Quantity -= factory.UpgradeCost;

                factory.UpgradeFinishTime = DateTime.Now.AddSeconds(factory.UpgradeTimeLength);

                if (system.PlayerId != playerId)
                {
                    return "You can only upgprade buildings built in your systems.";
                }

                data.SaveChanges();
            }

            return null;
        }
    }
}

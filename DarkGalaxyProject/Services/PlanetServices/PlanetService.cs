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
                    PlayerId = playerId,
                    IsTerraformed = p.IsTerraformed,
                    BuiltUpSpace = p.BuiltUpSpace,
                    BuildingCap = p.BuildingCap,
                    Factories = p.Factories.Select(f => new FactorySeviceModel
                    {
                        Id = f.Id,
                        Income = f.Income,
                        Level = f.Level,
                        UpgradeCost = f.UpgradeCost,
                        UpgradeFinishTime = f.UpgradeFinishTime,
                        UpgradeTimeLength = f.UpgradeTimeLength,
                        Type = f.FactoryType.ToString()
                    })
                    .ToList()
                })
                .First();

            return planet;
        }

        public string StartUpgrade(string buildingId, string planetId, string playerId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            if (factory.UpgradeFinishTime != null)
            {
                return "This building is already in the process of upgrading";
            }

            var planet = data.Planets.First(p => p.Id == planetId);
            var systemId = planet.SystemId;
            var milkyCoin = data.Resources.First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == systemId);
            var energy = data.Resources.First(r => r.Type == ResourceType.Energy && r.SystemId == systemId);
            var factoryStats = data.FactoryStats.First(f => f.FactoryType == factory.FactoryType && f.Level == factory.Level + 1);

            if(planet.BuiltUpSpace + factoryStats.BuildingSpace > planet.BuildingCap)
            {
                return "You don't have enough space on this planet to build that";
            }

            if (milkyCoin.Quantity < factory.UpgradeCost)
            {
                return $"You don't have enough {milkyCoin.Type.ToString()}.";
            }
            else if (energy.Quantity < factory.UpgradeCost / 10)
            {
                return $"You don't have enough {energy.Type.ToString()}.";
            }

            var system = data.Systems.First(s => s.Id == systemId);

            if (system.PlayerId != playerId)
            {
                return "You can only upgprade buildings built in your systems.";
            }

            milkyCoin.Quantity -= factory.UpgradeCost;
            energy.Quantity -= factory.UpgradeCost / 10;

            factory.UpgradeFinishTime = DateTime.Now.AddSeconds(factory.UpgradeTimeLength);
            data.SaveChanges();


            return $"You have started an upgrade for {factory.UpgradeCost} {milkyCoin.Type.ToString()} and {factory.UpgradeCost / 10} {energy.Type.ToString()}";
        }

        public string Terraform(string planetId, string playerId)
        {
            var terraformResearch = data.ResearchTrees.First(r => r.PlayerId == playerId && r.ResearchType == ResearchType.Terraforming);
            var planet = data.Planets.First(p => p.Id == planetId);

            if (planet.IsTerraformed)
            {
                return "This planet has already been terraformed";
            }

            if (!terraformResearch.IsLearned)
            {
                return "You have not yet learned the research required for terraforming";
            }

            var systemId = planet.SystemId;

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if (systemMilkyCoin.Quantity < planet.TerraformPrice)
            {
                return $"You don't have enough {systemMilkyCoin.Type.ToString()} to terraform this planet";
            }

            systemMilkyCoin.Quantity -= planet.TerraformPrice;

            planet.IsTerraformed = true;

            data.SaveChanges();

            return $"You have successfully terraformed this planet in exchange for {planet.TerraformPrice} {systemMilkyCoin.Type.ToString()}";
        }
    }
}

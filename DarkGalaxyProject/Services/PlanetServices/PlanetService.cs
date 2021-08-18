using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Services.PlanetServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlanetServices
{
    using static GlobalConstants.PlanetConstants;

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
                return BuildingAlreadyUpgrading;
            }

            var planet = data.Planets.First(p => p.Id == planetId);
            var systemId = planet.SystemId;
            var milkyCoin = data.Resources.First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == systemId);
            var energy = data.Resources.First(r => r.Type == ResourceType.Energy && r.SystemId == systemId);
            var factoryStats = data.FactoryStats.First(f => f.FactoryType == factory.FactoryType && f.Level == factory.Level + 1);

            if(planet.BuiltUpSpace + factoryStats.BuildingSpace > planet.BuildingCap)
            {
                return NotEnoughSpace;
            }

            if (milkyCoin.Quantity < factory.UpgradeCost)
            {
                return string.Format(NotEnoughResource, milkyCoin.Type.ToString());
            }
            else if (energy.Quantity < factory.UpgradeCost / 10)
            {
                return string.Format(NotEnoughResource, energy.Type.ToString());
            }

            var system = data.Systems.First(s => s.Id == systemId);

            if (system.PlayerId != playerId)
            {
                return CanOnlyUpgradeBuildingsInOwnSystem;
            }

            milkyCoin.Quantity -= factory.UpgradeCost;
            energy.Quantity -= factory.UpgradeCost / 10;

            factory.UpgradeFinishTime = DateTime.Now.AddSeconds(factory.UpgradeTimeLength);
            data.SaveChanges();

            return string.Format(UpgradeHasStarted, factory.UpgradeCost, milkyCoin.Type.ToString(), factory.UpgradeCost / 10, energy.Type.ToString());
        }

        public string Terraform(string planetId, string playerId)
        {
            var terraformResearch = data.ResearchTrees.First(r => r.PlayerId == playerId && r.ResearchType == ResearchType.Terraforming);
            var planet = data.Planets.First(p => p.Id == planetId);

            if (planet.IsTerraformed)
            {
                return PlanetAlreadyTerraformed;
            }

            if (!terraformResearch.IsLearned)
            {
                return TerraformingResearchNotLearned;
            }

            var systemId = planet.SystemId;

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if (systemMilkyCoin.Quantity < planet.TerraformPrice)
            {
                return string.Format(NotEnoughResource, systemMilkyCoin.Type.ToString());
            }

            systemMilkyCoin.Quantity -= planet.TerraformPrice;

            planet.IsTerraformed = true;

            data.SaveChanges();

            return string.Format(SuccessfullyTerraformedPlanet, planet.TerraformPrice, systemMilkyCoin.Type.ToString());
        }
    }
}

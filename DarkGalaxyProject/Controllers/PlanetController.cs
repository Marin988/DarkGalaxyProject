using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Controllers
{
    public class PlanetController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public PlanetController(ApplicationDbContext data, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult EnergyPlanet(string systemId)
        {
            var energyPlanet = data.EnergyPlanets
                .Where(p => p.SystemId == systemId)
                .Select(p => new EnergyPlanetViewModel
                {
                    Id = p.Id,
                    SystemId = p.SystemId,
                    Name = p.Name,
                    Type = p.Type.ToString(),
                    Position = p.Position,
                    FuelToEnergyCenter = new ResourceBuildingViewModel
                    {
                        Id = p.FuelToEnergyCenter.Id,
                        EnergyCost = p.FuelToEnergyCenter.EnergyCost,
                        Level = p.FuelToEnergyCenter.Level,
                        Production = p.FuelToEnergyCenter.Production,
                        Type = p.FuelToEnergyCenter.Type.ToString()
                    },
                    GeothermalPlant = new ResourceBuildingViewModel
                    {
                        Id = p.GeothermalPlant.Id,
                        EnergyCost = p.GeothermalPlant.EnergyCost,
                        Level = p.GeothermalPlant.Level,
                        Production = p.GeothermalPlant.Production,
                        Type = p.GeothermalPlant.Type.ToString()
                    },
                    SolarPanel = new ResourceBuildingViewModel
                    {
                        Id = p.SolarPanel.Id,
                        EnergyCost = p.SolarPanel.EnergyCost,
                        Level = p.SolarPanel.Level,
                        Production = p.SolarPanel.Production,
                        Type = p.SolarPanel.Type.ToString()
                    }
                })
                .First();

            return View(energyPlanet);
        }

        [Authorize]
        public IActionResult PopulatedPlanet(string systemId)
        {
            var populatedPlanet = data.PopulatedPlanets
                .Where(p => p.SystemId == systemId)
                .Select(p => new PopulatedPlanetViewModel
                {
                    Id = p.Id,
                    SystemId = p.SystemId,
                    Name = p.Name,
                    Position = p.Position,
                    Type = p.Type.ToString(),
                    Population = p.Population,
                    Amenity = new AmenityViewModel
                    {
                        Id = p.Amenities.Id,
                        CulturalIncrement = p.Amenities.CultureIncrement,
                        EnergyCost = p.Amenities.UpgradeCost,
                        Level = p.Amenities.Level
                    },
                    LivingQuarters = new LivingQuartersViewModel
                    {
                        Id = p.LivingQuarters.Id,
                        PopulationCapacity = p.LivingQuarters.PopulationCapacity,
                        UpgradeCost = p.LivingQuarters.UpgradeCost,
                        Level = p.LivingQuarters.Level
                    }
                })
                .First();

            return View(populatedPlanet);
        }

        [Authorize]
        public IActionResult ResourcePlanet(string systemId)
        {
            var resourcePlanet = data.ResourcePlanets
                .Where(p => p.SystemId == systemId)
                .Select(p => new ResourcePlanetViewModel
                {
                    Id = p.Id,
                    SystemId = p.SystemId,
                    Name = p.Name,
                    Position = p.Position,
                    Type = p.Type.ToString(),
                    TitaniumMine = new ResourceBuildingViewModel
                    {
                        Id = p.TitaniumMine.Id,
                        Level = p.TitaniumMine.Level,
                        EnergyCost = p.TitaniumMine.EnergyCost,
                        Production = p.TitaniumMine.Production,
                        Type = p.TitaniumMine.Type.ToString()
                    },
                    CrystalMine = new ResourceBuildingViewModel
                    {
                        Id = p.CrystalMine.Id,
                        Level = p.CrystalMine.Level,
                        EnergyCost = p.CrystalMine.EnergyCost,
                        Production = p.CrystalMine.Production,
                        Type = p.CrystalMine.Type.ToString()
                    },
                    FuelGenerator = new ResourceBuildingViewModel
                    {
                        Id = p.FuelGenerator.Id,
                        Level = p.FuelGenerator.Level,
                        EnergyCost = p.FuelGenerator.EnergyCost,
                        Production = p.FuelGenerator.Production,
                        Type = p.FuelGenerator.Type.ToString()
                    },
                    CrystalStorage = new StorageBuildingViewModel
                    {
                        Id = p.CrystalStorage.Id,
                        Level = p.CrystalStorage.Level,
                        EnergyCost = p.CrystalStorage.UpgradeCost,
                        ResourceCapacity = p.CrystalStorage.ResourceCapacity,
                        Type = p.CrystalStorage.Type.ToString()
                    },
                    FuelStorage = new StorageBuildingViewModel
                    {
                        Id = p.FuelStorage.Id,
                        Level = p.FuelStorage.Level,
                        EnergyCost = p.FuelStorage.UpgradeCost,
                        ResourceCapacity = p.FuelStorage.ResourceCapacity,
                        Type = p.FuelStorage.Type.ToString()
                    },
                    TitaniumStorage = new StorageBuildingViewModel
                    {
                        Id = p.TitaniumStorage.Id,
                        Level = p.TitaniumStorage.Level,
                        EnergyCost = p.TitaniumStorage.UpgradeCost,
                        ResourceCapacity = p.TitaniumStorage.ResourceCapacity,
                        Type = p.TitaniumStorage.Type.ToString()
                    }
                })
                .First();

            return View(resourcePlanet);
        }

        [Authorize]
        [HttpPost]
        public IActionResult LevelUp(string buildingId, string systemId, string planetType)
        {

            var resourceBuilding = data.ResourceBuildings.FirstOrDefault(b => b.Id == buildingId);
            var storageBuilding = data.StorageBuildings.FirstOrDefault(b => b.Id == buildingId);
            var amenity = data.Amenities.FirstOrDefault(b => b.Id == buildingId);
            var livingQuarters = data.LivingQuarters.FirstOrDefault(b => b.Id == buildingId);
            string planetId = null;

            if(resourceBuilding != null)
            {
                resourceBuilding.Level += 1;
                planetId = resourceBuilding.PlanetId;
            }
            else if(storageBuilding != null)
            {
                storageBuilding.Level += 1;
                planetId = storageBuilding.PlanetId;
            }
            else if (amenity != null)
            {
                amenity.Level += 1;
                planetId = amenity.PlanetId;
            }
            else if (livingQuarters != null)
            {
                livingQuarters.Level += 1;
                planetId = livingQuarters.PlanetId;
            }

            data.SaveChanges();

            return Redirect($"{planetType}?systemId={systemId}");
        }
    }
}

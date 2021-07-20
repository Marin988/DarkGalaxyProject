using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Data.Models.WithinSystem.Buildings;
using DarkGalaxyProject.Data.Models.WithinSystem.Planets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Seeders
{
    public class SystemsSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext data;

        public SystemsSeeder(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Seed()
        {
            if (this.data.Systems.Any())
            {
                return;
            }

            for (int i = 1; i <= 100; i++)
            {
                var system = new Data.Models.System { Position = i, Type = SystemType.Medium };

                var populatedPlanet = new PopulatedPlanet { Position = 1, Name = system.Position.ToString() + "-01", Type = PlanetType.Medium, SystemId = system.Id, Population = 7000 };

                var amenity = new Amenity { PlanetId = populatedPlanet.Id };

                var livingQuarters = new LivingQuarters { PlanetId = populatedPlanet.Id };

                var sun = new Sun { Name = system.Position.ToString() + "-01S", Size = 5000, Type = SunType.Dwarf, SystemId = system.Id };

                var resourcePlanet = new ResourcePlanet { Position = 2, Name = system.Position.ToString() + "-02", Type = PlanetType.Medium, SystemId = system.Id };

                var researchPlanet = new ResearchPlanet { Position = 3, Name = system.Position.ToString() + "-03", Type = PlanetType.Medium, SystemId = system.Id };

                var energyPlanet = new EnergyPlanet { Position = 4, Name = system.Position.ToString() + "-04", Type = PlanetType.Medium, SystemId = system.Id };

                var FuelToEnergyCenter = new ResourceBuilding { Type = ResourceBuildingType.Energy, PlanetId = energyPlanet.Id };
                var GeothermalPlant = new ResourceBuilding { Type = ResourceBuildingType.Energy, PlanetId = energyPlanet.Id };
                var SolarPanel = new ResourceBuilding { Type = ResourceBuildingType.Energy, PlanetId = energyPlanet.Id };

                var CrystalMine = new ResourceBuilding { Type = ResourceBuildingType.Crystal, PlanetId = resourcePlanet.Id };
                var FuelGenerator = new ResourceBuilding { Type = ResourceBuildingType.Fuel, PlanetId = resourcePlanet.Id };
                var TitaniumMine = new ResourceBuilding { Type = ResourceBuildingType.Titanium, PlanetId = resourcePlanet.Id };


                var TitaniumStorage = new StorageBuilding { Type = StorageBuildingType.Titanium, PlanetId = resourcePlanet.Id };
                var FuelStorage = new StorageBuilding { Type = StorageBuildingType.Fuel, PlanetId = resourcePlanet.Id };
                var CrystalStorage = new StorageBuilding { Type = StorageBuildingType.Crystal, PlanetId = resourcePlanet.Id };
                data.Systems.Add(system);
                data.Amenities.Add(amenity);
                data.LivingQuarters.Add(livingQuarters);
                data.PopulatedPlanets.Add(populatedPlanet);
                data.Suns.Add(sun);
                data.ResourcePlanets.Add(resourcePlanet);
                data.ResearchPlanets.Add(researchPlanet);
                data.EnergyPlanets.Add(energyPlanet);
                data.ResourceBuildings.Add(FuelToEnergyCenter);
                data.ResourceBuildings.Add(GeothermalPlant);
                data.ResourceBuildings.Add(SolarPanel);
                data.StorageBuildings.Add(TitaniumStorage);
                data.StorageBuildings.Add(FuelStorage);
                data.StorageBuildings.Add(CrystalStorage);


                data.SaveChanges();

                data.EnergyPlanets.First(ep => ep.Id == energyPlanet.Id).FuelToEnergyCenter = FuelToEnergyCenter;
                data.EnergyPlanets.First(ep => ep.Id == energyPlanet.Id).GeothermalPlant = GeothermalPlant;
                data.EnergyPlanets.First(ep => ep.Id == energyPlanet.Id).SolarPanel = SolarPanel;

                data.ResourcePlanets.First(p => p.Id == resourcePlanet.Id).CrystalMine = CrystalMine;
                data.ResourcePlanets.First(p => p.Id == resourcePlanet.Id).FuelGenerator = FuelGenerator;
                data.ResourcePlanets.First(p => p.Id == resourcePlanet.Id).TitaniumMine = TitaniumMine;

                data.ResourcePlanets.First(p => p.Id == resourcePlanet.Id).TitaniumStorage = TitaniumStorage;
                data.ResourcePlanets.First(p => p.Id == resourcePlanet.Id).FuelStorage = FuelStorage;
                data.ResourcePlanets.First(p => p.Id == resourcePlanet.Id).CrystalStorage = CrystalStorage;

                data.Systems.First(s => s.Id == system.Id).PopulatedPlanetId = populatedPlanet.Id;
                data.Systems.First(s => s.Id == system.Id).EnergyPlanetId = energyPlanet.Id;
                data.Systems.First(s => s.Id == system.Id).ResearchPlanetId = researchPlanet.Id;
                data.Systems.First(s => s.Id == system.Id).ResourcePlanetId = resourcePlanet.Id;

                //data.Systems.First(s => s.Id == system.Id).Resources = Resources;

                data.SaveChanges();
            }
        }
    }
}

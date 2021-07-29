using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
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
                var random = new Random();

                var systemType = random.Next(1, 4);

                var system = new Data.Models.System { Position = i, Type = (SystemType)systemType };

                var sunType = random.Next(1, 3);

                var sun = new Sun { Name = system.Position.ToString() + "-01S", Size = 5000, Type = (SunType)sunType, SystemId = system.Id };

                var planets = new Planet[4];

                var factories = new Factories[4];

                for (int j = 0; j < 4; j++)
                {
                    var planetType = random.Next(1, 4);

                    var planet = new Planet { Position = j + 1, Name = system.Position.ToString() + $"-0{j}", Type = (PlanetType)planetType, SystemId = system.Id };

                    var factory = new Factories { PlanetId = planet.Id };

                    planets[j] = planet;
                    factories[j] = factory;
                }

                var defences = new List<DefensiveStructure>();

                defences.Add(new DefensiveStructure(DefensiveStructureType.SpaceStation, system.Id));

                for (int z = 0; z < (int)system.Type * 5; z++)
                {
                    defences.Add(new DefensiveStructure(DefensiveStructureType.Satelite, system.Id));
                }

                foreach (var resource in system.Resources)
                {
                    resource.Quantity = 10000 * (int)system.Type;
                }

                var shipBuilders = new List<ShipBuilder>();

                foreach (var item in Enum.GetValues(typeof(ShipType)))
                {
                    shipBuilders.Add(new ShipBuilder
                    {
                        Count = 0,
                        FinishedBuildingTime = null,
                        ShipType = (ShipType)item,
                        SystemId = system.Id
                    });
                }

                var defenceBuilders = new List<DefenceBuilder>();

                foreach (var item in Enum.GetValues(typeof(DefensiveStructureType)))
                {
                    defenceBuilders.Add(new DefenceBuilder
                    {
                        FinishedBuildingTime = null,
                        DefensiveStructureType = (DefensiveStructureType)item,
                        SystemId = system.Id,
                        Count = 0
                    });
                }

                var fleets = new List<Fleet>();

                for (int j = 0; j < 5; j++)
                {
                    fleets.Add(new Fleet(system.Id));
                }

                data.Systems.Add(system);
                data.Suns.Add(sun);
                data.Planets.AddRange(planets);
                data.Factories.AddRange(factories);
                data.DefenceBuilders.AddRange(defenceBuilders);
                data.ShipBuilders.AddRange(shipBuilders);
                data.DefensiveStructures.AddRange(defences);
                data.Fleets.AddRange(fleets);

                data.SaveChanges();
            }
        }
    }
}

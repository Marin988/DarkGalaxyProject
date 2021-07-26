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

                var systemType = random.Next(0, 2);

                var system = new Data.Models.System { Position = i, Type = (SystemType)systemType };

                var sunType = random.Next(1, 2);

                var sun = new Sun { Name = system.Position.ToString() + "-01S", Size = 5000, Type = (SunType)sunType, SystemId = system.Id };

                var planets = new Planet[4];

                var factories = new Factories[4];

                for (int j = 0; j < 4; j++)
                {
                    var planetType = random.Next(1, 3);

                    var planet = new Planet { Position = j + 1, Name = system.Position.ToString() + $"-0{j}", Type = (PlanetType)planetType, SystemId = system.Id };

                    var factory = new Factories { PlanetId = planet.Id };

                    planets[j] = planet;
                    factories[j] = factory;
                }

                var GoliathBuilder = new ShipBuilder
                {
                    FinishedBuildingTime = null,
                    ShipType = ShipType.Goliath,
                    SystemId = system.Id,
                    Count = 0
                };
                var VengeanceBuilder = new ShipBuilder
                {
                    FinishedBuildingTime = null,
                    ShipType = ShipType.Vengeance,
                    SystemId = system.Id,
                    Count = 0
                };
                var LeonovBuilder = new ShipBuilder
                {
                    FinishedBuildingTime = null,
                    ShipType = ShipType.Leonov,
                    SystemId = system.Id,
                    Count = 0
                };


                data.Systems.Add(system);
                data.Suns.Add(sun);
                data.Planets.AddRange(planets);
                data.Factories.AddRange(factories);
                data.ShipBuilders.AddRange(GoliathBuilder, VengeanceBuilder, LeonovBuilder);

                data.SaveChanges();
            }
        }
    }
}

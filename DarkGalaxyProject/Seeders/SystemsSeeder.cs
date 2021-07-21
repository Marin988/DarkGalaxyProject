using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
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
                var system = new Data.Models.System { Position = i, Type = SystemType.Medium };

                var sun = new Sun { Name = system.Position.ToString() + "-01S", Size = 5000, Type = SunType.Dwarf, SystemId = system.Id };

                var Planet = new Planet { Position = 2, Name = system.Position.ToString() + "-01", Type = PlanetType.Medium, SystemId = system.Id };


                data.Systems.Add(system);
                data.Suns.Add(sun);

                data.SaveChanges();
            }
        }
    }
}

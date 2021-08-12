using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Contracts
{
    interface IDatabaseSeeder
    {
        public void Seed();

        public Task SeedUsers();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlanetServices
{
    public class PlanetServiceModel
    {
        public string Id { get; set; }

        public string PlayerId { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public FactoriesServiceModel Factories { get; set; }
    }
}

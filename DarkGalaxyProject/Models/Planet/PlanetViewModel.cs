using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Planet
{
    public class PlanetViewModel
    {
        public string Id { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public FactoriesViewModel Factories { get; set; }
    }
}

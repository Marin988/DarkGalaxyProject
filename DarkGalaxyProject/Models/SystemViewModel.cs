using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class SystemViewModel
    {
        public string Id { get; set; }

        public int Position { get; set; }

        public string Type { get; set; }

        public PopulatedPlanetViewModel PopulatedPlanet { get; set; }

        public EnergyPlanetViewModel EnergyPlanet { get; set; }

        public ResourcePlanetViewModel ResourcePlanet { get; set; }

        public IEnumerable<ShipViewModel> Ships { get; set; }
    }
}

using System.Collections.Generic;

namespace DarkGalaxyProject.Services.PlanetServices.Models
{
    public class PlanetServiceModel
    {
        public string Id { get; set; }

        public string PlayerId { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsTerraformed { get; set; }

        public int BuiltUpSpace { get; set; }

        public int BuildingCap { get; set; }

        public IEnumerable<FactorySeviceModel> Factories { get; set; }
    }
}

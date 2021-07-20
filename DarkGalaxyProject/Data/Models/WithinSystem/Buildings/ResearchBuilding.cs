using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Buildings
{
    public class ResearchBuilding
    {
        public ResearchBuilding()
        {
            Researches = new List<Research>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public IEnumerable<Research> Researches { get; set; }
    }
}

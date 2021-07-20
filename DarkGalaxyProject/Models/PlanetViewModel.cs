using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class PlanetViewModel
    {
        public string Id { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string SystemId { get; set; }

        public string PlanetType => this.GetType().Name.Replace("ViewModel", "");
    }
}

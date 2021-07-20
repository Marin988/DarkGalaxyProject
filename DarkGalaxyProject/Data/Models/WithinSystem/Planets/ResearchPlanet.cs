using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Planets
{
    public class ResearchPlanet : Planet
    {
        public ResearchBuilding ResearchBuilding { get; set; }
    }
}

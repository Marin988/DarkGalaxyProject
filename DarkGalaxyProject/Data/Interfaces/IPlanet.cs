using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkGalaxyProject.Data.Models;

namespace DarkGalaxyProject.Data.Interfaces
{
    public interface IPlanet
    {

        public PlanetType Type { get; set; }
    }
}

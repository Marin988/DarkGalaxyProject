using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Test.Data
{
    public class PlanetData
    {
        public static Planet Planet(string planetId, string systemId)
            => new Planet { Id = planetId, SystemId = systemId };

        public static Factories Factories(string planetId, string factoryId)
            => new Factories(0, 0, 0, 0, 0, FactoryType.Factory, planetId);
    }
}

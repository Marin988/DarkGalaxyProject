using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Planets
{
    public class PopulatedPlanet : Planet
    {

        public int Population { get; set; }

        public Amenity Amenities { get; set; }

        public LivingQuarters LivingQuarters { get; set; }
    }
}

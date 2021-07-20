using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class PopulatedPlanetViewModel : PlanetViewModel
    {
        public int Population { get; set; }

        public AmenityViewModel Amenity { get; set; }

        public LivingQuartersViewModel LivingQuarters { get; set; }
    }
}


//{ Position = 1, Name = system.Position.ToString() + "-01", Type = PlanetType.Medium, System = system, SystemId = system.Id, Population = 7000, Amenities = amenity, LivingQuarters = livingQuarters };
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Data.Models.WithinSystem.Planets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models
{
    public class System
    {
        public System()
        {
            Resources = new List<Resource>()
            {
                new Resource { Quantity = 0, Type = ResourceType.Culture },
                new Resource { Quantity = 0, Type = ResourceType.Culture },
                new Resource { Quantity = 0, Type = ResourceType.Energy },
                new Resource { Quantity = 0, Type = ResourceType.Fuel },
                new Resource { Quantity = 0, Type = ResourceType.Knowledge },
                new Resource { Quantity = 0, Type = ResourceType.MilkyCoin },
                new Resource { Quantity = 0, Type = ResourceType.Titanium }
            };
            Suns = new List<Sun>();
            DefensiveStructures = new List<DefensiveStructure>();
            BlackHoles = new List<BlackHole>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        //[Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Range(1, 1000)]
        public int Position { get; init; }

        [Required]
        [EnumDataType(typeof(SystemType))]
        public SystemType Type { get; set; }

        public string UserId { get; set; }
        public Player User { get; set; }

        public string AllianceId { get; set; }
        public Alliance Alliance { get; set; }

        public IEnumerable<Resource> Resources { get; set; }

        public string PopulatedPlanetId { get; set; }
        public string ResourcePlanetId { get; set; }
        public string ResearchPlanetId { get; set; }
        public string EnergyPlanetId { get; set; }
        public PopulatedPlanet PopulatedPlanet { get; set; }
        public ResourcePlanet ResourcePlanet { get; set; }
        public ResearchPlanet ResearchPlanet { get; set; }
        public EnergyPlanet EnergyPlanet { get; set; }

        public IEnumerable<Sun> Suns { get; set; }

        public IEnumerable<DefensiveStructure> DefensiveStructures { get; set; }

        public IEnumerable<BlackHole> BlackHoles { get; set; }

        public IEnumerable<Ship> Ships { get; set; }
    }
}

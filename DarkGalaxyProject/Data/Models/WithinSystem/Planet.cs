using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Planet : IPlanet
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int Position { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public PlanetType Type { get; set; }

        public bool IsTerraformed { get; set; }

        public int BuiltUpSpace { get; set; }

        public int BuildingCap => 5000 * (int)Type;

        public IEnumerable<Factories> Factories { get; set; }    

        [Required]
        public string SystemId { get; set; }
        public System System { get; set; }
    }
}
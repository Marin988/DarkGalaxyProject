using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    using static DataConstants.Planet;

    public class Planet 
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public int Position { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public PlanetType Type { get; set; }

        [Required]
        public bool IsTerraformed { get; set; }

        public int TerraformPrice => TerraformPriceConst / (int)Type;

        public int BuiltUpSpace { get; set; }

        public int BuildingCap => BuildingCapConst / (int)Type;

        public IEnumerable<Factories> Factories { get; set; }    

        [Required]
        public string SystemId { get; set; }
        public System System { get; set; }
    }
}
﻿using DarkGalaxyProject.Data.Models.WithinSystem.Planets;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem.Buildings
{
    public class Amenity
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int CultureIncrement => 10 * Level;

        public int UpgradeCost => 100 * Level;

        public int Level { get; set; }

        [Required]
        public string PlanetId { get; set; }

        public PopulatedPlanet Planet { get; set; }
    }
}

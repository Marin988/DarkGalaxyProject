﻿using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class DefenceBuilder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string SystemId { get; set; }

        [Required]
        public DefensiveStructureType DefensiveStructureType { get; set; }

        [Range(1, 1000)]
        public int Count { get; set; }

        public int PricePerUnit => (int)DefensiveStructureType * 1000;

        public int TotalPrice => PricePerUnit * Count;

        public int BuildTime => (int)DefensiveStructureType * 5 * Count;

        public DateTime? FinishedBuildingTime { get; set; }
    }
}

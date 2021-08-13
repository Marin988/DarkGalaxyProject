﻿using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class ShipBuilder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string SystemId { get; set; }

        [Required]
        public ShipType ShipType { get; set; }

        public int Count { get; set; }

        public int Price => (int)ShipType * 1000 * Count;

        public int BuildTime => (int)ShipType * Count;

        public DateTime? FinishedBuildingTime { get; set; }
    }
}
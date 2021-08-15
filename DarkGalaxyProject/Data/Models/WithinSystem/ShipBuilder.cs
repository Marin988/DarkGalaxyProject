using DarkGalaxyProject.Data.Enums;
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

        [Range(1, 1000)]
        public int Count { get; set; }

        public int TotalPrice => PricePerShip * Count;

        public int PricePerShip { get; init; }

        public int BuildTime { get; init; }

        public DateTime? FinishedBuildingTime { get; set; }
    }
}
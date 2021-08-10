using DarkGalaxyProject.Data.Enums;
using System;

namespace DarkGalaxyProject.Data.Models
{
    public class ShipBuilder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string SystemId { get; set; }

        public ShipType ShipType { get; set; }

        public int Count { get; set; }

        public int BuildTime => (int)ShipType * Count;

        public DateTime? FinishedBuildingTime { get; set; }
    }
}
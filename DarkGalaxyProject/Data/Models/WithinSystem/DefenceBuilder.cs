using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    using static DataConstants.Builder;

    public class DefenceBuilder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string SystemId { get; set; }

        [Required]
        public DefensiveStructureType DefensiveStructureType { get; set; }

        [Range(MinCount, MaxCount)]
        public int Count { get; set; }

        public int PricePerUnit { get; init; }

        public int TotalPrice => PricePerUnit * Count;

        public int BuildTime { get; init; }

        public DateTime? FinishedBuildingTime { get; set; }
    }
}

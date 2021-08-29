using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Fleet
    {
        public Fleet(string systemId)
        {
            Ships = new List<Ship>();
            SystemId = systemId;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int? DestinationSystemPoistion { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public bool Outgoing { get; set; }

        public MissionType MissionType { get; set; }

        [Required]
        public string SystemId { get; set; }

        public IEnumerable<Ship> Ships { get; set; }
    }
}

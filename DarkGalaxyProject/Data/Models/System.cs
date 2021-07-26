﻿using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.WithinSystem;
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
                new Resource { Quantity = 0, Type = ResourceType.Fuel },
                new Resource { Quantity = 0, Type = ResourceType.MilkyCoin },
            };
            Suns = new List<Sun>();
            DefensiveStructures = new List<DefensiveStructure>();
            Planets = new List<Planet>();
            Ships = new List<Ship>();
            ShipBuildingQueue = new List<ShipBuilder>();
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

        public IEnumerable<Planet> Planets { get; set; }

        public IEnumerable<Sun> Suns { get; set; }

        public IEnumerable<DefensiveStructure> DefensiveStructures { get; set; }

        public IEnumerable<Ship> Ships { get; set; }

        public IEnumerable<ShipBuilder> ShipBuildingQueue { get; set; }

        public int DestinationSystemPoistion { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public DateTime? DepartureTime { get; set; }

        public bool Outgoing { get; set; }
    }
}

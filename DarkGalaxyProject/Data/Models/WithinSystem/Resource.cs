using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Resource
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ResourceType Type { get; init; }

        public int Quantity { get; set; }

        public string SystemId { get; set; }
    }
}

using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Resource
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ResourceType Type { get; init; }

        public int Quantity { get; set; }

        public string PlayerId { get; set; }

        public string SystemId { get; set; }
    }
}

using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Sun
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public SunType Type { get; set; }

        public int Size { get; set; }

        [Required]
        public string SystemId { get; set; }
        public System System { get; set; }
    }
}

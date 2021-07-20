using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

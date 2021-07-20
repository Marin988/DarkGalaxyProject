using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class BlackHole 
    {
        public BlackHole()
        {
            Resources = new List<Resource>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public BlackHoleType Type { get; set; }

        public int Size { get; set; }

        public int Storage { get; set; }

        public IEnumerable<Resource> Resources { get; set; }

        [Required]
        public string SystemId { get; set; }
        public System System { get; set; }
    }
}

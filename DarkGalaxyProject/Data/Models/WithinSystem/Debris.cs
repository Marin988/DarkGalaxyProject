using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Debris
    {
        public Debris()
        {
            Resources = new List<Resource>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public IEnumerable<Resource> Resources { get; set; }

        public int Timer { get; set; }

        [Required]
        public DebrisSizeType SizeType { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    public class ResearchTree
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public bool Goliath { get; set; }

        public bool Vengeance { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }
    }
}

using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models
{
    public class Alliance
    {
        public Alliance(string name)
        {
            Members = new List<Player>();
            Candidates = new List<Player>();
            Messages = new List<ChatMessage>();
            Name = name;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        [MinLength(6)]
        [MaxLength(120)]
        public string Description { get; set; }

        public IEnumerable<Player> Members { get; set; }

        public IEnumerable<Player> Candidates { get; set; }

        public IEnumerable<ChatMessage> Messages { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public Player Leader { get; set; }
    }
}

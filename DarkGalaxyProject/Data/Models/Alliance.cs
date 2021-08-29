using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models
{
    using static DataConstants.Alliance;

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
        [MaxLength(NameMaxLength)]
        [MinLength(NameMinLength)]
        public string Name { get; set; }

        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public IEnumerable<Player> Members { get; set; }

        public IEnumerable<Player> Candidates { get; set; }

        public IEnumerable<ChatMessage> Messages { get; set; }

        [Required]
        public string LeaderId { get; set; }
        public Player Leader { get; set; }
    }
}

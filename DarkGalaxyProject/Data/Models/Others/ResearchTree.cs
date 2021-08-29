using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.Others
{
    public class ResearchTree
    {
        public ResearchTree(string playerId, ResearchType researchType, string name, string description, int price)
        {
            IsLearned = false;
            PlayerId = playerId;
            ResearchType = researchType;
            Name = name;
            Description = description;
            Price = price;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public bool IsLearned { get; set; }

        public string Name { get; init; }

        [Required]
        public ResearchType ResearchType { get; set; }

        public string Description { get; init; }

        public int Price { get; init; }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }
    }
}

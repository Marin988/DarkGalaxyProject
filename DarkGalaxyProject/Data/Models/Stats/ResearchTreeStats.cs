using DarkGalaxyProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.Stats
{
    public class ResearchTreeStats
    {
        public ResearchTreeStats(ResearchType researchType, bool isLearned, string name, string description, int price)
        {
            ResearchType = researchType;
            IsLearned = isLearned;
            Name = name;
            Description = description;
            Price = price;
        }

        [Key]
        [Required]
        public ResearchType ResearchType { get; init; }

        public bool IsLearned { get; init; }

        public string Name { get; init; }

        public string Description { get; init; }

        public int Price { get; init; }
    }
}

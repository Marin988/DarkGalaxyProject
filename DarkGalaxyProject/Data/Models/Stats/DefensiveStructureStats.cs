using DarkGalaxyProject.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.Stats
{
    public class DefensiveStructureStats
    {
        public DefensiveStructureStats(DefensiveStructureType type, int maxHP, int damage, int price, int buildTime)
        {
            Type = type;
            MaxHP = maxHP;
            Damage = damage;
            Price = price;
            BuildTime = buildTime;
        }

        [Key]
        [Required]
        public DefensiveStructureType Type { get; init; }

        public int MaxHP { get; init; }

        public int Damage { get; init; }

        public int Price { get; init; }

        public int BuildTime { get; init; }
    }
}

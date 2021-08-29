using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class DefensiveStructure
    {
        public DefensiveStructure(DefensiveStructureType type, string systemId, int maxHP, int damage)
        {
            Type = type;
            SystemId = systemId;
            HP = maxHP;
            MaxHP = maxHP;
            Damage = damage;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int HP { get; set; }

        public int MaxHP { get; init; }

        public int Damage { get; init; }

        [Required]
        public DefensiveStructureType Type { get; set; }

        [Required]
        public string SystemId { get; set; }
        public System System { get; set; }
    }
}

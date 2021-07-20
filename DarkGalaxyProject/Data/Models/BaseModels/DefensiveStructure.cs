using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.BaseModels
{
    public class DefensiveStructure
    {
        public DefensiveStructure(DefensiveStructureType type, string systemId)
        {
            Type = type;
            SystemId = systemId;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int HitPoints => 1000 / (int)Type;

        public int Damage => 1000 / (int)Type;

        [Required]
        public DefensiveStructureType Type { get; set; }

        [Required]
        public string SystemId { get; set; }
        public System System { get; set; }

        public string AllianceId { get; set; }
        public Alliance Alliance { get; set; }
    }
}

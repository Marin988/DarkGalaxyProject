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

        public bool Discovery { get; set; }

        public bool Shields { get; set; }

        public bool ReadyForBattle { get; set; }

        public bool FuelOptimization { get; set; }

        public bool EnergyBarriers { get; set; }

        public bool SpaceCascades { get; set; }

        public bool BattleShips { get; set; }

        public bool SpaceMonster { get; set; }

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }
    }
}

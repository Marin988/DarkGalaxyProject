using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    public class ResearchTree
    {
        public ResearchTree(string playerId, ResearchType researchType)
        {
            IsLearned = false;
            PlayerId = playerId;
            ResearchType = researchType;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public bool IsLearned { get; set; }

        public string Name { get; set; }//maybe i don't need a name if I can use researchType as name

        public ResearchType ResearchType { get; set; }

        public string Description { get; set; }

        //image

        public int Price => 10000 * (int)ResearchType;

        [Required]
        public string PlayerId { get; set; }

        public Player Player { get; set; }
    }
}

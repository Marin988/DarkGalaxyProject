using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlayerServices
{
    public class ResearchServiceModel
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public bool IsLearned { get; set; }

        public string ResearchType { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public string PlayerId { get; set; }
    }
}

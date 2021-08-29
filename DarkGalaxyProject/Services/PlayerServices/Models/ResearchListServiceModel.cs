using System.Collections.Generic;

namespace DarkGalaxyProject.Services.PlayerServices.Models
{
    public class ResearchListServiceModel
    {
        public IEnumerable<ResearchServiceModel> Researches { get; set; }

        public string CurrentSystemId { get; set; }

        public string PlayerId { get; set; }
    }
}

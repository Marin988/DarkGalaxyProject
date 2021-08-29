using DarkGalaxyProject.Services.AllianceServices.Models;
using System.Collections.Generic;

namespace DarkGalaxyProject.Models.Alliance
{
    public class AllAlliancesViewModel
    {
        public IEnumerable<AllianceServiceModel> Alliances { get; set; }

        public bool IsInAlliance { get; set; }
    }
}

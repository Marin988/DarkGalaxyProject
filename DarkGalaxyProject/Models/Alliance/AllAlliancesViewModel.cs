using DarkGalaxyProject.Services.AllianceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class AllAlliancesViewModel
    {
        public IEnumerable<AllianceServiceModel> Alliances { get; set; }

        public bool IsInAlliance { get; set; }
    }
}

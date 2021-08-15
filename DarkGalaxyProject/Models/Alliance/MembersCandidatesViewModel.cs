using DarkGalaxyProject.Services.AllianceServices;
using DarkGalaxyProject.Services.AllianceServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class MembersCandidatesViewModel
    {
        public IEnumerable<MemberServiceModel> Members { get; set; }
        public IEnumerable<MemberServiceModel> Candidates { get; set; }

        public string AllianceId { get; set; }

        public string AllianceLeaderId { get; set; }
    }
}

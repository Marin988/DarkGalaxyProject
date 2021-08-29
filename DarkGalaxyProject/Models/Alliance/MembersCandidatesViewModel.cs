using DarkGalaxyProject.Services.AllianceServices.Models;
using System.Collections.Generic;

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

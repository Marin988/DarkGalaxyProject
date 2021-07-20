using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class MembersCandidatesViewModel
    {
        public List<MemberViewModel> Members { get; set; }

        public List<MemberViewModel> Candidates { get; set; }

        public string allianceId { get; set; }
    }
}

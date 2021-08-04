using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.AllianceServices
{
    public interface IAllianceService
    {
        public IEnumerable<AllianceServiceModel> All();

        public AllianceServiceModel Home(string playerId);

        public bool IsInAlliance(string playerId);

        public IEnumerable<MemberServiceModel> Members(string allianceId);//+2 models? or just + 1!

        public IEnumerable<MemberServiceModel> Candidates(string allianceId);//+2 models? or just + 1!

        public IEnumerable<ChatMessageServiceModel> ChatMessages(string allianceId);

        public bool Send(string allianceId, string content, string playerId);
        public bool Create(string playerId, string allianceName);
        public bool Leave(string playerId);
        public bool AcceptCandidate(string allianceId, string candidateId);
        public bool Apply(string allianceId, string playerId);
        public bool PromoteToLeader(string allianceId, string playerId);


    }
}

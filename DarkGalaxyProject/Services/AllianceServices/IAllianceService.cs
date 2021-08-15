using DarkGalaxyProject.Services.AllianceServices.Models;
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

        public string Send(string allianceId, string content, string playerId);
        public bool Create(string playerId, string allianceName);
        public bool Leave(string playerId);
        public string AcceptCandidate(string allianceId, string candidateId, string playerId);
        public bool Apply(string allianceId, string playerId);
        public string PromoteToLeader(string allianceId, string playerId, string leaderId);

        public string ChangeDescription(string allianceId, string description);

        public string LeaderId(string allianceId);

    }
}

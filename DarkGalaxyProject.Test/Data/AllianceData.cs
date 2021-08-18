using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Test.Data
{
    public class AllianceData
    {
        public static IEnumerable<Alliance> FiveAlliances(string allianceId)
            => Enumerable.Range(0, 5).Select(d => new Alliance("alliance")
            {
                Id = allianceId + d
            });

        public static IEnumerable<Player> Members(string allianceId)
            => Enumerable.Range(0, 5).Select(m => new Player
            {
                AllianceId = allianceId
            });

        public static IEnumerable<Player> Candidates(string allianceId)
        => Enumerable.Range(0, 5).Select(m => new Player
        {
            AllianceCandidateId = allianceId
        });

        public static IEnumerable<ChatMessage> ChatMessages(string allianceId)
            => Enumerable.Range(0, 5).Select(m => new ChatMessage
            {
                AllianceId = allianceId
            });

        public static Alliance AllianceWithLeader(string leaderId, string allianceId)
            => new Alliance("allianceName") { LeaderId = leaderId, Id = allianceId };

        public static Player AllianceMember(string playerId, string allianceId)
            => new Player { Id = playerId, AllianceId = allianceId };

        public static Player AllianceLeader(string playerId, string allianceId)
            => new Player { Id = playerId, AllianceLeaderId = allianceId };
    }
}

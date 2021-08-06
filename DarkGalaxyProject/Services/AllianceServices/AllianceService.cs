using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkGalaxyProject.Data.Models;

namespace DarkGalaxyProject.Services.AllianceServices
{
    public class AllianceService : IAllianceService
    {
        private readonly ApplicationDbContext data;

        public AllianceService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<AllianceServiceModel> All()
        {
            var alliances = data.Alliances
                .Select(a => new AllianceServiceModel
                {
                    Id = a.Id,
                    Leader = a.Leader.UserName,
                    MembersCount = a.Members.Count(),
                    Name = a.Name,
                })
                .ToList();

            return alliances;
        }

        public string AcceptCandidate(string allianceId, string candidateId, string playerId)
        {
            var candidate = data.Players.First(p => p.Id == candidateId);
            var alliance = data.Alliances.First(a => a.Id == allianceId);

            if(playerId != alliance.LeaderId)
            {
                return "Only the leader can accept members!";
            }

            candidate.AllianceCandidateId = null;

            candidate.AllianceId = allianceId;

            data.SaveChanges();

            return null;
        }

        public bool Apply(string allianceId, string playerId)
        {
            var candidate = data.Players.First(p => p.Id == playerId);

            candidate.AllianceCandidateId = allianceId;

            data.SaveChanges();

            return true;
        }

        public IEnumerable<ChatMessageServiceModel> ChatMessages(string allianceId)
        {
            var messages = data.ChatMessages
                .Where(c => c.AllianceId == allianceId)
                .Select(c => new ChatMessageServiceModel
                {
                    AllianceId = c.AllianceId,
                    Content = c.Content,
                    Sender = c.Player.UserName,
                    SenderId = c.PlayerId,
                    TimeOfSending = c.TimeOfSending
                })
                .ToList();

            return messages;
        }

        public bool Create(string playerId, string allianceName)
        {
            var player = data.Players.First(p => p.Id == playerId);

            var alliance = new Alliance(allianceName);

            alliance.Members.ToList().Add(player);

            data.Alliances.Add(alliance);

            data.SaveChanges();

            player.AllianceId = alliance.Id;

            alliance.LeaderId = playerId;
            player.AllianceLeaderId = alliance.Id;

            data.SaveChanges();

            return true;
        }

        public AllianceServiceModel Home(string playerId)
        {
            var allianceId = data.Players.First(u => u.Id == playerId).AllianceId;

            if (allianceId == null)
            {
                return null;
            }

            var alliance = data.Alliances
                .Where(a => a.Id == allianceId)
                .Select(a => new AllianceServiceModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Leader = a.Leader.UserName,
                    MembersCount = a.Members.Count()
                })
                .First();

            return alliance;
        }

        public bool IsInAlliance(string playerId)
        {
            if(data.Players.First(p => p.Id == playerId).AllianceId == null)
            {
                return false;
            }

            return true;
        }

        public bool Leave(string playerId)
        {
            var player = data.Players.First(p => p.Id == playerId);
            var alliance = data.Alliances.First(a => a.Id == player.AllianceId);

            if(alliance.LeaderId == playerId)
            {
                data.Alliances.Remove(alliance);
            }

            player.AllianceId = null;

            data.SaveChanges();

            return true;
        }

        public IEnumerable<MemberServiceModel> Members(string allianceId)
        {
            var members = data.Players
                .Where(p => p.AllianceId == allianceId)
                .Select(p => new MemberServiceModel
                {
                    Id = p.Id,
                    Systems = p.Systems.Count(),
                    UserName = p.UserName
                })
                .ToList();

            return members;
        }

        public IEnumerable<MemberServiceModel> Candidates(string allianceId)
        {
            var candidates = data.Players
                .Where(p => p.AllianceCandidateId == allianceId)
                .Select(p => new MemberServiceModel
                {
                    Id = p.Id,
                    Systems = p.Systems.Count(),
                    UserName = p.UserName
                })
                .ToList();

            return candidates;
        }

        public bool PromoteToLeader(string allianceId, string playerId)
        {
            var player = data.Players.First(p => p.Id == playerId);

            player.AllianceLeaderId = allianceId;

            data.SaveChanges();

            return true;
        }

        public bool Send(string allianceId, string content, string playerId)
        {
            data.ChatMessages.Add(new ChatMessage
            {
                AllianceId = allianceId,
                Content = content,
                PlayerId = playerId
            });

            data.SaveChanges();

            return true;
        }
    }
}

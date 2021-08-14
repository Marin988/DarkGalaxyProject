using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Services.AllianceServices.Models;

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

            player.AllianceId = alliance.Id;

            alliance.LeaderId = playerId;
            player.AllianceLeaderId = alliance.Id;

            data.Alliances.Add(alliance);

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
                    MembersCount = a.Members.Count(),
                    Description = a.Description,
                    LeaderId = a.Leader.Id
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
            var chatMessages = data.ChatMessages.Where(c => c.AllianceId == alliance.Id);

            if (alliance.LeaderId == playerId)
            {
                player.AllianceId = null;
                data.Alliances.Remove(alliance);
            }

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

        public string PromoteToLeader(string allianceId, string playerId, string leaderId)
        {
            var player = data.Players.First(p => p.Id == playerId);
            var leader = data.Players.First(p => p.Id == leaderId);
            var alliance = data.Alliances.First(a => a.Id == allianceId);

            if(leaderId != alliance.Leader.Id)
            {
                return "Only the leader can promote other members";
            }

            if(player.AllianceId != allianceId)
            {
                return "You can only promote members of this alliance";
            }

            if (player.Id == alliance.Leader.Id)
            {
                return "You are already the leader of this alliance";
            }

            leader.AllianceLeaderId = null;

            leader.AllianceId = allianceId;

            player.AllianceLeaderId = allianceId;

            data.SaveChanges();

            return null;
        }

        public string Send(string allianceId, string content, string playerId)
        {
            if(content == null)
            {
                return "Message should contain at least one letter or digit";
            }

            data.ChatMessages.Add(new ChatMessage
            {
                AllianceId = allianceId,
                Content = content,
                PlayerId = playerId
            });

            data.SaveChanges();

            return null;
        }

        public string ChangeDescription(string allianceId, string description)
        {
            var alliance = data.Alliances
                .First(a => a.Id == allianceId);

            if(description == null || description.Length < 6 || description.Length > 120)
            {
                return "Description should be between 6 and 120 symbols!";
            }

            alliance.Description = description;

            data.SaveChanges();

            return null;
        }
    }
}

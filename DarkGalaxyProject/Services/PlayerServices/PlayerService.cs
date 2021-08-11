using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlayerServices
{
    public class PlayerService : IPlayerService
    {
        private readonly ApplicationDbContext data;

        public PlayerService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public MessageServiceModel Message(string messageId)
        {
            var message = data.Messages
                .Where(m => m.Id == messageId)
                .Select(m => new MessageServiceModel
                {
                    Content = m.Content,
                    SenderId = m.SenderId,
                    SenderName = m.Sender.UserName,
                    Title = m.Title,
                    Time = m.TimeOfSending
                })
                .First();

            return message;
        }

        public IEnumerable<MessageListingServiceModel> PlayerMessages(string playerId)
        {
            var messages = data.Messages
                .Where(m => m.ReceiverId == playerId || m.SenderId == playerId)
                .Select(m => new MessageListingServiceModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReceiverId = m.ReceiverId == playerId ? m.SenderId : m.ReceiverId,
                    ReceiverName = m.ReceiverId == playerId ? m.Sender.UserName : m.Receiver.UserName,
                    Time = m.TimeOfSending
                })
                .ToList()
                .OrderByDescending(m => m.Time)
                .ToList();

            return messages;
        }

        public bool PlayerResearches(string playerId)
        {

            var researches = new List<ResearchTree>();

            foreach (var researchType in Enum.GetValues(typeof(ResearchType)))
            {
                researches.Add(new ResearchTree(playerId, (ResearchType)researchType));
            }

            data.ResearchTrees.AddRange(researches);
            data.SaveChanges();

            return true;
        }

        public ProfileServiceModel Profile(string playerId)
        {
            var player = data.Players.Where(p => p.Id == playerId)
                .Select(p => new ProfileServiceModel
                {
                    UserName = p.NormalizedUserName,
                    AllianceName = p.Alliance.Name,
                    Systems = p.Systems.Count()
                })
                .FirstOrDefault();

            return player;
        }

        public ResearchListServiceModel Researches(string playerId)
        {
            var researches = data.ResearchTrees
                .Where(r => r.PlayerId == playerId)
                .Select(r => new ResearchServiceModel
                {
                    Id = r.Id,
                    Description = r.Description,
                    IsLearned = r.IsLearned,
                    PlayerId = r.PlayerId,
                    Price = r.Price,
                    ResearchType = r.ResearchType.ToString()
                })
                .ToList();

            var currentSystemId = data.Players.First(p => p.Id == playerId).CurrentSystemId;

            var researchListModel = new ResearchListServiceModel
            {
                Researches = researches,
                CurrentSystemId = currentSystemId,
                PlayerId = playerId
            };

            return researchListModel;
        }



        public string SendMessage(string content, string receiverName, string senderId, string title)
        {
            var receiverId = data.Players.FirstOrDefault(p => p.UserName == receiverName).Id;
            if(receiverId == null)
            {
                return "There is no player with such name";
            }

            data.Messages.Add(new Message
            {
                Content = content,
                ReceiverId = data.Players.First(p => p.UserName == receiverName).Id,
                SenderId = senderId,
                Title = title,
                TimeOfSending = DateTime.Now
            });

            data.SaveChanges();

            return null;
        }

        public Data.Models.System StartingSystem()
        {
            var systemForUser = data.Systems.First(s => s.PlayerId == null && s.Type == SystemType.Small);
            return systemForUser;
        }

        public string StudyResearch(string researchId, string systemId)
        {
            var research = data.ResearchTrees.FirstOrDefault(r => r.Id == researchId);

            var systemMilkyCoin = data.Resources.FirstOrDefault(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if(research.IsLearned == true)
            {
                return "You have already done this research!";
            }

            if(systemMilkyCoin.Quantity < research.Price)
            {
                return $"You don't have enough {systemMilkyCoin.Type.ToString()}";
            }

            systemMilkyCoin.Quantity -= research.Price;
            research.IsLearned = true;

            data.SaveChanges();

            return "";
        }
    }
}

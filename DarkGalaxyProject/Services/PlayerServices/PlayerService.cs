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

        public bool SendMessage(string content, string receiverName, string senderId, string title)
        {
            data.Messages.Add(new Message
            {
                Content = content,
                ReceiverId = data.Players.First(p => p.UserName == receiverName).Id,
                SenderId = senderId,
                Title = title,
                TimeOfSending = DateTime.Now
            });

            data.SaveChanges();

            return true;
        }

        public Data.Models.System StartingSystem()
        {
            var systemForUser = data.Systems.First(s => s.PlayerId == null);
            return systemForUser;
        }
    }
}

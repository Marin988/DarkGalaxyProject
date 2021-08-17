using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Services.PlayerServices.Models;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<ProfileServiceModel> AllPlayers()
        {
            var allPlayers = data.Players
                .Include(p => p.Systems)
                .Select(p => new ProfileServiceModel
                {
                    UserName = p.UserName,
                    AllianceName = p.Alliance != null ? p.Alliance.Name : null,
                    Systems = p.Systems.Count()
                })
                .ToList();

            return allPlayers;
        }

        public MessageServiceModel Message(string messageId)
        {
            var message = data.Messages
                .Where(m => m.Id == messageId)
                .Select(m => new MessageServiceModel
                {
                    Content = m.Content,
                    SenderId = m.SenderId,
                    SenderName = m.Sender.UserName == null ? "System" : m.Sender.UserName,
                    Title = m.Title,
                    Time = m.TimeOfSending
                })
                .First();

            return message;
        }

        public AllMessagesServiceModel PlayerMessages(string playerId, int page)
        {
            var messages = data.Messages
                .Where(m => m.ReceiverId == playerId || m.SenderId == playerId)
                .OrderByDescending(m => m.TimeOfSending)
                .Skip((page - 1) * 5)
                .Take(5)
                .Select(m => new MessageListingServiceModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReceiverId = m.ReceiverId == playerId ? m.SenderId : m.ReceiverId,
                    ReceiverName = m.ReceiverId == playerId ? m.Sender.UserName : m.Receiver.UserName,
                    Time = m.TimeOfSending,
                    Seen = m.ReceiverId == playerId ? m.Seen : true,
                    IsReceived = m.ReceiverId == playerId
                })
                .ToList();

            var allMessagesCount = data.Messages
                .Where(m => m.ReceiverId == playerId || m.SenderId == playerId)
                .Count();

            var allPagesCount = allMessagesCount / 5;
            if(allMessagesCount % 5 > 0)
            {
                allPagesCount += 1;
            }

            if(allMessagesCount == 0)
            {
                allPagesCount = 1;
            }

            var allMessages = new AllMessagesServiceModel
            {
                Messages = messages,
                Page = page,
                AllPagesCount = allPagesCount,
                PlayerId = playerId
            };


            return allMessages;
        }

        public bool PlayerResearches(string playerId)
        {

            var researches = new List<ResearchTree>();

            foreach (var researchType in Enum.GetValues(typeof(ResearchType)))
            {
                var researchStats = data.ResearchTreeStats.First(r => r.ResearchType == (ResearchType)researchType);

                researches.Add(new ResearchTree(playerId, (ResearchType)researchType, researchStats.Name, researchStats.Description, researchStats.Price));
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

        public string ReadMessage(string messageId)
        {
            var message = data.Messages
                .First(m => m.Id == messageId);

            message.Seen = true;

            data.SaveChanges();

            return null;
        }

        public ResearchListServiceModel Researches(string playerId)
        {
            var researches = data.ResearchTrees
                .Where(r => r.PlayerId == playerId)
                .ToList()
                .OrderBy(r => r.Price)
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
            var receiver = data.Players.FirstOrDefault(p => p.UserName == receiverName);
            if(receiver == null)
            {
                return "There is no player with such name";
            }

            if(receiver.Id == senderId)
            {
                return "You can't send messages to yourself";
            }

            data.Messages.Add(new Message
            {
                Content = content,
                ReceiverId = receiver.Id,
                SenderId = senderId,
                Title = title,
                TimeOfSending = DateTime.Now,
                Seen = false
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

            var systemPaper = data.Resources.FirstOrDefault(r => r.SystemId == systemId && r.Type == ResourceType.Paper);

            if(research.IsLearned == true)
            {
                return "You have already done this research!";
            }

            if(systemPaper.Quantity < research.Price)
            {
                return $"You don't have enough {systemPaper.Type.ToString()}";
            }

            systemPaper.Quantity -= research.Price;
            research.IsLearned = true;

            data.SaveChanges();

            return null;
        }
    }
}

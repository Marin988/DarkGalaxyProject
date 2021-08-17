using DarkGalaxyProject.Services.PlayerServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlayerServices
{
    public interface IPlayerService
    {
        public IEnumerable<ProfileServiceModel> AllPlayers();

        public MessageServiceModel Message(string messageId);

        public AllMessagesServiceModel PlayerMessages(string playerId, int page);

        public ProfileServiceModel Profile(string playerId);

        public Data.Models.System StartingSystem();

        public bool PlayerResearches(string playerId);

        public string SendMessage(string content, string receiverName, string senderId, string title);

        public ResearchListServiceModel Researches(string playerId);

        public string StudyResearch(string researchId, string systemId);

        public string ReadMessage(string messageId);

    }
}

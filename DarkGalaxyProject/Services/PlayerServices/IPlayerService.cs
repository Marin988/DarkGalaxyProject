using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlayerServices
{
    public interface IPlayerService
    {
        public MessageServiceModel Message(string messageId);

        public IEnumerable<MessageListingServiceModel> PlayerMessages(string playerId);

        public ProfileServiceModel Profile(string playerId);

        public Data.Models.System StartingSystem();

        public bool PlayerResearches(string playerId);

        public bool SendMessage(string content, string receiverName, string senderId, string title);

    }
}

//Content = message.Content,
//                ReceiverId = data.Players.First(p => p.UserName == message.ReceiverName).Id,
//                SenderId = message.SenderId,
//                Title = message.Title,
//                TimeOfSending = DateTime.Now

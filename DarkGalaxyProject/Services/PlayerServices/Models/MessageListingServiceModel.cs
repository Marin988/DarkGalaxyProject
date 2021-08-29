using System;

namespace DarkGalaxyProject.Services.PlayerServices.Models
{
    public class MessageListingServiceModel
    {
        public string Id { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        public bool Seen { get; set; }

        public DateTime? Time { get; set; }

        public string Title { get; set; }

        public bool IsReceived { get; set; }
    }
}

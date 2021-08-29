using System;

namespace DarkGalaxyProject.Models.Alliance
{
    public class ChatViewMessageModel
    {
        public string Content { get; set; }

        public DateTime TimeOfSending { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public string AllianceId { get; set; }
    }
}

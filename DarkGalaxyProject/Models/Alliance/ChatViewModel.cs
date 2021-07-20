using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class ChatViewModel
    {
        public IEnumerable<ChatViewMessageModel> Messages { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public string AllianceId { get; set; }

        public string Content { get; set; }
    }
}

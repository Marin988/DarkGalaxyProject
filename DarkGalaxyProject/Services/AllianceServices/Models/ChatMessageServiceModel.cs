using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.AllianceServices.Models
{
    public class ChatMessageServiceModel
    {
        public string Content { get; set; }

        public DateTime TimeOfSending { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public string AllianceId { get; set; }
    }
}

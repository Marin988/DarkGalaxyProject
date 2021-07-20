using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class AllianceChatMessageViewModel
    {
        public string Content { get; set; }

        public DateTime TimeOfSending { get; set; }

        public string Sender { get; set; }

        public string SenderId { get; set; }

        public string AllianceId { get; set; }
    }
}

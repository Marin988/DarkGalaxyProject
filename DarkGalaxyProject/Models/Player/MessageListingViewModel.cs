using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Player
{
    public class MessageListingViewModel
    {
        public string Id { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverName { get; set; }

        public DateTime? Time { get; set; }

        public string Title { get; set; }
    }
}

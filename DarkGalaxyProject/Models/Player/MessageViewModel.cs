using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Player
{
    public class MessageViewModel
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public string SenderName { get; set; }

        public DateTime? Time { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }
    }
}

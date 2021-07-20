using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Player
{
    public class MessageFormModel
    {
        public string SenderId { get; set; }

        public string ReceiverName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

    }
}

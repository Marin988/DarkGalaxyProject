using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlayerServices.Models
{
    public class AllMessagesServiceModel
    {
        public IEnumerable<MessageListingServiceModel> Messages { get; set; }

        public int Page { get; set; }

        public int AllPagesCount { get; set; }

        public string PlayerId { get; set; }
    }
}

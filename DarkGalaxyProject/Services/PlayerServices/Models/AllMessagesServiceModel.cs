using System.Collections.Generic;

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

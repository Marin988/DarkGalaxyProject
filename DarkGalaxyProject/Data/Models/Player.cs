using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DarkGalaxyProject.Data.Models.Others;
using Microsoft.AspNetCore.Identity;

namespace DarkGalaxyProject.Data.Models
{
    public class Player : IdentityUser
    {
        public Player()
        {
            Systems = new List<System>();
            SentMessages = new List<Message>();
            ReceivedMessages = new List<Message>();
            ResearcheTree = new List<ResearchTree>();
        }

        public string AllianceId { get; set; }

        public Alliance Alliance { get; set; }

        public string AllianceLeaderId { get; set; }

        public Alliance AllianceLeader { get; set; }

        public string AllianceCandidateId { get; set; }
        public Alliance AllianceCandidate { get; set; }

        [Required]
        public string CurrentSystemId { get; set; }

        public System CurrentSystem { get; set; }

        public IEnumerable<System> Systems { get; set; }

        public IEnumerable<Message> SentMessages { get; set; }

        public IEnumerable<Message> ReceivedMessages { get; set; }

        public IEnumerable<ResearchTree> ResearcheTree { get; set; }

        public IEnumerable<AuctionDeal> SoldAuctionDeals { get; set; }

        public IEnumerable<AuctionDeal> BoughtAuctionDeals { get; set; }
    }
}

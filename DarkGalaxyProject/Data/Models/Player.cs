using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using Microsoft.AspNetCore.Identity;

namespace DarkGalaxyProject.Data.Models
{
    public class Player : IdentityUser
    {
        public Player()
        {
            Resources = new List<Resource>()
            {
                new Resource { Quantity = 0, Type = ResourceType.Culture },
                new Resource { Quantity = 0, Type = ResourceType.Energy },
                new Resource { Quantity = 0, Type = ResourceType.Fuel },
                new Resource { Quantity = 0, Type = ResourceType.Knowledge },
                new Resource { Quantity = 0, Type = ResourceType.MilkyCoin },
                new Resource { Quantity = 0, Type = ResourceType.Titanium }
            };
            Systems = new List<System>();
            SentMessages = new List<Message>();
            ReceivedMessages = new List<Message>();
            Technologies = new List<Technology>();
            Researches = new List<Research>();

        }

        [Required]
        public string Password { get; set; }

        public ResearchTree ResearchTree { get; set; }

        public string AllianceId { get; set; }

        public Alliance Alliance { get; set; }

        public string AllianceLeaderId { get; set; }

        public Alliance AllianceLeader { get; set; }

        public string AllianceLeadersId { get; set; }

        public Alliance AllianceLeaders { get; set; }

        public string AllianceCandidateId { get; set; }
        public Alliance AllianceCandidate { get; set; }

        public IEnumerable<Resource> Resources { get; set; }

        public IEnumerable<System> Systems { get; set; }

        public IEnumerable<Message> SentMessages { get; set; }

        public IEnumerable<Message> ReceivedMessages { get; set; }

        public IEnumerable<Technology> Technologies { get; set; }

        public IEnumerable<Research> Researches { get; set; }
    }
}

using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.BaseModels
{
    public class Alliance
    {
        public Alliance(string name)
        {
            Members = new List<Player>();
            Messages = new List<ChatMessage>();
            Name = name;
            Leaders = new List<Player>();
            Resources = new List<Resource>();
            Systems = new List<System>();
            Ships = new List<Ship>();
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        public AllianceType Type { get; set; } // => (AllianceType)Enum.Parse(typeof(AllianceType), this.GetType().Name);// and remove get and set to make it work....

        public IEnumerable<Player> Members { get; set; }

        public IEnumerable<Player> Candidates { get; set; }

        public IEnumerable<ChatMessage> Messages { get; set; }

        public string LeaderId { get; set; }
        public Player Leader { get; set; }

        public IEnumerable<Player> Leaders { get; set; }

        public IEnumerable<Resource> Resources { get; set; }

        public IEnumerable<System> Systems { get; set; }

        public IEnumerable<Ship> Ships { get; set; }
    }
}

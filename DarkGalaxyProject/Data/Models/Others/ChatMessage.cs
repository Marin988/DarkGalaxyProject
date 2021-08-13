using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    public class ChatMessage : IMessage
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ChatMessageType Type { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime TimeOfSending { get; } = DateTime.Now;

        [Required]
        public string PlayerId { get; set; }
        public Player Player { get; set; }


        [Required]
        public string AllianceId { get; set; }

        public Alliance Alliance { get; set; }
    }
}

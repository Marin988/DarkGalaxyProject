using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.Others
{
    using static DataConstants.ChatMessage;

    public class ChatMessage : IMessage
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ChatMessageType Type { get; set; }

        [Required]
        [StringLength(MaxLength, MinimumLength = MinLength)]
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

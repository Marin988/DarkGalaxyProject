using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    public class Message : IMessage
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime? TimeOfSending { get; } = DateTime.UtcNow;

        [Required]
        public string SenderId { get; set; }

        //[ForeignKey("SenderId")]
        public Player Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        //[ForeignKey("ReceiverId")]
        public Player Receiver { get; set; }
    }
}

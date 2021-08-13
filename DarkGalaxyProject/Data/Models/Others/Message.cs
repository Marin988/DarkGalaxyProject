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
        [StringLength(20, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(240, MinimumLength = 5)]
        public string Content { get; set; }

        public DateTime TimeOfSending { get; set; }

        public string SenderId { get; set; }

        public Player Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public Player Receiver { get; set; }
    }
}

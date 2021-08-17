using DarkGalaxyProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    using static DataConstants.Message;

    public class Message : IMessage
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [StringLength(ContentMaxLength, MinimumLength = ContentMinLength)]
        public string Content { get; set; }

        public bool Seen { get; set; }

        public DateTime TimeOfSending { get; set; }

        public string SenderId { get; set; }

        public Player Sender { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        public Player Receiver { get; set; }
    }
}

using DarkGalaxyProject.Services.AllianceServices.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Models.Alliance
{
    public class ChatFormViewModel
    {
        public IEnumerable<ChatMessageServiceModel> Messages { get; set; }

        public string Player { get; set; }

        public string PlayerId { get; set; }

        public string AllianceId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

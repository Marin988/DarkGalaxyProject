using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Models.Player
{
    public class MessageFormModel
    {
        public string SenderId { get; set; }

        [Required]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [StringLength(240, MinimumLength = 5)]
        public string Content { get; set; }

    }
}

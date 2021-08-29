using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Models.Player
{
    public class RegisterFormModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

    }
}

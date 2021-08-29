using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Models.Player
{
    public class LoginFormModel
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
    }
}

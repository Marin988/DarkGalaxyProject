using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Models.Alliance
{
    public class CreateFormModel
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}

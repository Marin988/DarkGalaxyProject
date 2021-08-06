using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class CreateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}

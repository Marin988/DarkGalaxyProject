using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class BuildShipViewModel
    {
        [Required]
        [EnumDataType(typeof(ShipType))]
        public string Type { get; set; }

        public int Quantity { get; set; }
    }
}

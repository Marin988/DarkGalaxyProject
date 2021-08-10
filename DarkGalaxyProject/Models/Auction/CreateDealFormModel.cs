using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Auction
{
    public class CreateDealFormModel
    {
        [Required]
        public int Price { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public string ShipType { get; set; }

        [Required]
        public int ShipCount { get; set; }
    }
}

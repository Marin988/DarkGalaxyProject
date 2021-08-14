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
        [Range(100, 10000000)]
        public int Price { get; set; }

        [Required]
        public string SellerId { get; set; }

        [Required]
        public string ShipType { get; set; }

        [Required]
        [Range(1, 1000)]
        public int ShipCount { get; set; }
    }
}

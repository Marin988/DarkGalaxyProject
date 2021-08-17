using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models
{
    public class AuctionDeal
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Range(1, 1000)]
        public int Quantity { get; set; } 

        [Required]
        public string ShipType { get; set; }

        [Range(100, 10000000)]
        public int Price { get; set; }

        public string BuyerId { get; set; }

        public Player Buyer { get; set; }

        [Required]
        public string SellerId { get; set; }

        public Player Seller { get; set; }

        public IEnumerable<Ship> ShipsForSale { get; set; }
    }
}

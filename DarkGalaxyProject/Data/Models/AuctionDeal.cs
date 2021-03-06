using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models
{
    using static DataConstants.AuctionDeal;

    public class AuctionDeal
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Range(MinQuantity, MaxQuantity)]
        public int Quantity { get; set; } 

        [Required]
        public string ShipType { get; set; }

        [Range(MinPrice, MaxPrice)]
        public int Price { get; set; }

        public string BuyerId { get; set; }

        public Player Buyer { get; set; }

        [Required]
        public string SellerId { get; set; }

        public Player Seller { get; set; }

        public IEnumerable<Ship> ShipsForSale { get; set; }
    }
}

﻿using DarkGalaxyProject.Data.Enums;
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

        public int Quantity { get; set; } 

        public string ShipType { get; set; }

        public int Price { get; set; }

        public string BuyerId { get; set; }

        public Player Buyer { get; set; }

        [Required]
        public string SellerId { get; set; }

        public Player Seller { get; set; }

        public IEnumerable<Ship> ShipsForSale { get; set; }//later might change that to IEnumerable<object>
        //may also add dates (do I need two dates? For when it was released for selling and when it was sold)
    }
}
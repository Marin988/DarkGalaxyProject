using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Auction
{
    public class CreateDealFormModel
    {
        public int Price { get; set; }

        public string SellerId { get; set; }

        public string ShipType { get; set; }

        public int ShipCount { get; set; }
    }
}

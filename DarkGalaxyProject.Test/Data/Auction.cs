using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Test.Data
{
    public static class Auction
    {
        public static IEnumerable<AuctionDeal> TenOpenDeals()
            => Enumerable.Range(0, 10).Select(d => new AuctionDeal
            {

            });

        public static IEnumerable<Ship> FiveShipsOfPlayer(string shiptype, string playerId)
            => Enumerable.Range(0, 5).Select(s => new Ship((ShipType)Enum.Parse(typeof(ShipType), shiptype), null, playerId, 0, 0, 0, 0, 0));

        public static AuctionDeal Deal(string dealId, string sellerId, string shiptype, int price)
            => new AuctionDeal { Id = dealId, SellerId = sellerId, ShipType = shiptype, Price = price};

        public static IEnumerable<Ship> ShipsInDeal(string shiptype, string playerId, string dealId)
            => Enumerable.Range(0, 5).Select(s => new Ship((ShipType)Enum.Parse(typeof(ShipType), shiptype), null, playerId, 0, 0, 0, 0, 0) { DealId = dealId });
    }
}

using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.Auction
{
    public class AuctionService : IAuctionService
    {
        private readonly ApplicationDbContext data;

        public AuctionService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<DealServiceModel> All()
        {
            var allDeals = data.AuctionDeals
                .Where(ad => ad.BuyerId == null)
                .Select(ad => new DealServiceModel
                {
                    Id = ad.Id,
                    SellerName = ad.Seller.UserName,
                    Price = ad.Price,
                    Quantity = ad.Quantity,
                    SellerId = ad.SellerId,
                    ShipType = ad.ShipsForSale.Select(s => s.Type.ToString()).FirstOrDefault()
                })
                .ToList();

            return allDeals;
        }

        public bool Buy(string dealId, string buyerId)
        {
            var deal = data.AuctionDeals
                .Include(a => a.ShipsForSale)
                .First(ad => ad.Id == dealId);

            var buyerCurrentSystemId = data.Players
                .First(p => p.Id == buyerId)
                .CurrentSystemId;

            var buyerMilkyCoin = data.Resources
                .First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == buyerCurrentSystemId);

            var sellerCurrentSystemId = data.Players.First(p => p.Id == deal.SellerId).CurrentSystemId;

            var sellerMilkyCoin = data.Resources
                .First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == sellerCurrentSystemId);

            deal.BuyerId = buyerId;

            if (buyerMilkyCoin.Quantity < deal.Price)
            {
                //TODO: error not enough money
                return false;
            }

            buyerMilkyCoin.Quantity -= deal.Price;
            sellerMilkyCoin.Quantity += deal.Price;

            foreach (var ship in deal.ShipsForSale)
            {
                ship.DealId = null;
                ship.SystemId = buyerCurrentSystemId;
                ship.PlayerId = buyerId;
            }

            data.SaveChanges();

            return true;
        }

        public string CreateDeal(int price, string sellerId, int quantity, string shipType)
        {
            var shipsForSale = ShipsForSale(sellerId, shipType, quantity);

            var deal = new AuctionDeal
            {
                Price = price,
                SellerId = sellerId,
                ShipsForSale = shipsForSale,
                Quantity = quantity,
                ShipType = shipType
            };

            data.AuctionDeals.Add(deal);

            data.SaveChanges();

            foreach (var ship in shipsForSale)//would that be done by EF Core?
            {
                ship.DealId = deal.Id;
            }

            data.SaveChanges();

            return deal.Id;
        }

        public bool DeleteDeal(string dealId)
        {
            var deal = data.AuctionDeals
                .FirstOrDefault(ad => ad.Id == dealId);

            if(deal == null)
            {
                return false;
            }

            var DealShips = data.Ships.Where(s => s.DealId == dealId).ToList();

            foreach (var ship in DealShips)//would that be taken care of by EF Core?
            {
                ship.DealId = null;
            }

            data.AuctionDeals.Remove(deal);

            data.SaveChanges();

            return true;
        }

        public IEnumerable<Ship> ShipsForSale(string playerId, string shipType, int quantity)
        {
            var shipsForSale = data.Ships
                .Where(s => s.PlayerId == playerId && s.Type == (ShipType)Enum.Parse(typeof(ShipType), shipType) && !s.OnMission)
                .Take(quantity)
                .ToList();

            return shipsForSale;
        }
    }
}

using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.AuctionServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkGalaxyProject.Services.AuctionServices
{
    using static GlobalConstants.AuctionConstants;

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

        public string Buy(string dealId, string buyerId)
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
                return string.Format(NotEnoughCoin, deal.Price, buyerMilkyCoin.Type.ToString(), buyerMilkyCoin.Quantity);
            }

            buyerMilkyCoin.Quantity -= deal.Price;
            sellerMilkyCoin.Quantity += deal.Price;

            var shipsCount = deal.ShipsForSale.Count();
            var shipsType = deal.ShipsForSale.First().Type.ToString();

            foreach (var ship in deal.ShipsForSale)
            {
                ship.DealId = null;
                ship.SystemId = buyerCurrentSystemId;
                ship.PlayerId = buyerId;
            }

            var sellerMessageTitle = "Sold item";
            var sellerMessageContent = $"{deal.Quantity} of {shipsType} were sold for {deal.Price}.";
            var buyerMessageTitle = "Bought item";
            var buyerMessageContent = $"You bought {deal.Quantity} of {shipsType} for {deal.Price}.";

            ReportMessage(deal.SellerId, sellerMessageTitle, sellerMessageContent);
            ReportMessage(deal.BuyerId, buyerMessageTitle, buyerMessageContent);

            return string.Format(SuccessfulPurchase, shipsCount, shipsType);
        }

        public string CreateDeal(int price, string sellerId, int quantity, string shipType)
        {
            var shipTypeEnum = (ShipType)Enum.Parse(typeof(ShipType), shipType);
            var shipsForSale = ShipsForSale(sellerId, shipTypeEnum, quantity);

            if (shipsForSale.Count() != quantity)
            {
                return string.Format(NotEnoughShips, shipsForSale.Count(), shipTypeEnum.ToString());
            }

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

            foreach (var ship in shipsForSale)
            {
                ship.DealId = deal.Id;
            }

            data.SaveChanges();

            return null;
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

            foreach (var ship in DealShips)
            {
                ship.DealId = null;
            }

            data.AuctionDeals.Remove(deal);

            data.SaveChanges();

            return true;
        }

        public IEnumerable<Ship> ShipsForSale(string playerId, ShipType shipType, int quantity)
        {
            var shipsForSale = data.Ships
                .Where(s => s.PlayerId == playerId && s.Type == shipType && !s.OnMission && s.DealId == null)
                .Take(quantity)
                .ToList();

            return shipsForSale;
        }

        private void ReportMessage(string playerId, string messageTitle, string messageContent)
        {
            var reportMessage = new Message()
            {
                ReceiverId = playerId,
                TimeOfSending = DateTime.Now,
                Title = messageTitle,
                Content = messageContent,
                Seen = false
            };

            data.Messages.Add(reportMessage);
            data.SaveChanges();
        }
    }
}

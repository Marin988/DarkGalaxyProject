using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Models.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using DarkGalaxyProject.Data.Enums;

namespace DarkGalaxyProject.Controllers
{
    public class AuctionController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;

        public AuctionController(ApplicationDbContext data, UserManager<Player> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var allDeals = data.AuctionDeals
                .Where(ad => ad.BuyerId == null)
                .Select(ad => new DealListView
                {
                    Id = ad.Id,
                    SellerName = ad.Seller.UserName,
                    Price = ad.Price,
                    Quantity = ad.Quantity,
                    SellerId = ad.SellerId,
                    ShipType = ad.ShipsForSale.Select(s => s.Type.ToString()).FirstOrDefault()
                })
                .ToList();

            return View(allDeals);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewInfo = new CreateDealFormModel
            {
                SellerId = userManager.GetUserId(User)
            };

            return View(viewInfo);
        }
        
        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateDealFormModel dealModel)
        {
            var shipsForSale = data.Ships
                .Where(s => s.PlayerId == userManager.GetUserId(User) && s.Type == (ShipType)Enum.Parse(typeof(ShipType), dealModel.ShipType) && !s.OnMission)
                .Take(dealModel.ShipCount)
                .ToList();


            var deal = new AuctionDeal
            {
                Price = dealModel.Price,
                SellerId = dealModel.SellerId,
                ShipsForSale = shipsForSale,
                Quantity = dealModel.ShipCount,
                ShipType = dealModel.ShipType
            };

            data.AuctionDeals.Add(deal);

            data.SaveChanges();

            foreach (var ship in shipsForSale)//would that be done by EF Core?
            {
                ship.DealId = deal.Id;
            }

            data.SaveChanges();

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(string dealId)
        {
            var deal = data.AuctionDeals
                .First(ad => ad.Id == dealId);

            var shipsForSale = data.Ships.Where(s => s.DealId == dealId).ToList();

            foreach (var ship in shipsForSale)//would that be taken care of by EF Core?
            {
                ship.DealId = null;
            }

            data.AuctionDeals.Remove(deal);

            data.SaveChanges();

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Buy(string dealId)
        {
            var playerId = userManager.GetUserId(User);

            var deal = data.AuctionDeals
                .Include(a => a.ShipsForSale)
                .First(ad => ad.Id == dealId);

            var buyerCurrentSystemId = data.Players
                .First(p => p.Id == playerId)
                .CurrentSystemId;

            var buyerMilkyCoin = data.Resources
                .First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == buyerCurrentSystemId);

            var sellerCurrentSystemId = data.Players.First(p => p.Id == deal.SellerId).CurrentSystemId;

            var sellerMilkyCoin = data.Resources
                .First(r => r.Type == ResourceType.MilkyCoin && r.SystemId == sellerCurrentSystemId);

            deal.BuyerId = playerId;

            buyerMilkyCoin.Quantity -= deal.Price;
            sellerMilkyCoin.Quantity += deal.Price;

            if(buyerMilkyCoin.Quantity < deal.Price)
            {
                //TODO: error not enough money
            }

            foreach (var ship in deal.ShipsForSale)
            {
                ship.DealId = null;
                ship.SystemId = buyerCurrentSystemId;
                ship.PlayerId = playerId;
            }

            data.SaveChanges();

            return Redirect("All");
        }
    }
}

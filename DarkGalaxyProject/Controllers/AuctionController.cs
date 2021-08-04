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
using DarkGalaxyProject.Services.Auction;

namespace DarkGalaxyProject.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService auctionDeals;
        private readonly UserManager<Player> userManager;

        public AuctionController(UserManager<Player> userManager, IAuctionService auctionDeals)
        {
            this.userManager = userManager;
            this.auctionDeals = auctionDeals;
        }

        [Authorize]
        public IActionResult All()
        {
            var allDeals = auctionDeals.All();

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
            auctionDeals.CreateDeal(dealModel.Price, userManager.GetUserId(User), dealModel.ShipCount, dealModel.ShipType);

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(string dealId)
        {
            auctionDeals.DeleteDeal(dealId);

            //if false return error

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Buy(string dealId)
        {
            var playerId = userManager.GetUserId(User);

            auctionDeals.Buy(dealId, playerId);

            //if false return error

            return Redirect("All");
        }
    }
}

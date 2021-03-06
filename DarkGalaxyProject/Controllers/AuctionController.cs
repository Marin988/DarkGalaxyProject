using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Models.Auction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DarkGalaxyProject.Services.AuctionServices;

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
            var message = auctionDeals.CreateDeal(dealModel.Price, userManager.GetUserId(User), dealModel.ShipCount, dealModel.ShipType);

            TempData["Message"] = message;

            if (message != null)
            {
                return Redirect("Create");
            }

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

            var message = auctionDeals.Buy(dealId, playerId);

            TempData["Message"] = message;

            return Redirect("All");
        }
    }
}

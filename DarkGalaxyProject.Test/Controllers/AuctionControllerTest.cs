
using DarkGalaxyProject.Controllers;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models.Auction;
using DarkGalaxyProject.Services.AuctionServices;
using DarkGalaxyProject.Services.AuctionServices.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DarkGalaxyProject.Test.Controllers
{
    using static Data.Auction;
    using static Data.Players;
    using static GlobalConstants.AuctionConstants;

    public class AuctionControllerTest
    {
        const string playerId = "playerId";
        const string playerId2 = "playerId2";
        const string dealId = "dealId";
        const string systemId = "systemId";
        const string systemId2 = "systemId2";
        const int price = 5000;
        const int dealPrice = 10000;
        const int shipCount = 1;
        const string shipTypeBattleship = nameof(ShipType.BattleShip);

        [Fact]
        public void AllShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<AuctionController>
            .Instance(instance => instance
                .WithData(TenOpenDeals()))
            .Calling(c => c.All())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<IEnumerable<DealServiceModel>>());

        [Fact]//I should check for the SellerId which should be a part of this view
        public void CreateShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<AuctionController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<CreateDealFormModel>());

        [Fact]
        public void CreateShouldBeForAuthorizedUsersAndRedirectToAllAndCreateADeal()
            => MyController<AuctionController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId))
                .WithData(FiveShipsOfPlayer(shipTypeBattleship, playerId)))
            .Calling(c => c.Create(new CreateDealFormModel
            {
                Price = price,
                SellerId = playerId,
                ShipCount = shipCount,
                ShipType = shipTypeBattleship
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<AuctionDeal>(deals => deals
                    .Any(d =>
                        d.Price == price &&
                        d.SellerId == playerId &&
                        d.ShipsForSale.Count() == shipCount &&
                        d.ShipType == shipTypeBattleship)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("All");

        [Fact]
        public void DeleteShouldBeForAuthorizedUsersAndRedirectToAllAndDeleteADeal()
            => MyController<AuctionController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId))
                .WithData(Deal(dealId, playerId, shipTypeBattleship, dealPrice))
                .WithData(ShipsInDeal(shipTypeBattleship, playerId, dealId)))
            .Calling(c => c.Delete(dealId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<AuctionDeal>(deals => deals
                    .Count() == 0)
                .WithSet<Ship>(ships => ships
                    .All(s => s.DealId == null)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("All");

        [Fact]
        public void BuyShouldBeForAuthorizedUsersAndRedirectToAllAndSetBuyerId()//I am not testing the payment
            => MyController<AuctionController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(PlayerWithCurrentSystem(playerId, systemId))
                .WithData(PlayerWithCurrentSystem(playerId2, systemId2))
                .WithData(System(playerId, systemId))
                .WithData(System(playerId2, systemId2))
                .WithData(Deal(dealId, playerId2, shipTypeBattleship, dealPrice))
                .WithData(ShipsInDeal(shipTypeBattleship, playerId2, dealId)))
            .Calling(c => c.Buy(dealId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<AuctionDeal>(deals => deals
                    .Any(d => d.BuyerId == playerId &&
                        d.ShipsForSale.Count() == 0 &&
                        d.SellerId == playerId2))
                .WithSet<Ship>(ships => ships
                    .All(s => s.DealId == null)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("All");

        [Fact]
        public void BuyShouldReturnCorrectTempdataIfBuyerCantPay()
            => MyController<AuctionController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(PlayerWithCurrentSystem(playerId, systemId))
                .WithData(PlayerWithCurrentSystem(playerId2, systemId2))
                .WithData(System(playerId, systemId))
                .WithData(System(playerId2, systemId2))
                .WithData(Deal(dealId, playerId2, shipTypeBattleship, dealPrice))
                .WithData(ShipsInDeal(shipTypeBattleship, playerId2, dealId)))
            .Calling(c => c.Buy(dealId))
            .ShouldHave()
            .TempData(d => d.ContainingEntryWithValue(string.Format(NotEnoughCoin, dealPrice, ResourceType.MilkyCoin.ToString(), 5000)));

        [Fact]
        public void BuyShouldReturnCorrectTempdataUponSuccessfulPurchase()
            => MyController<AuctionController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(PlayerWithCurrentSystem(playerId, systemId))
                .WithData(PlayerWithCurrentSystem(playerId2, systemId2))
                .WithData(System(playerId, systemId))
                .WithData(System(playerId2, systemId2))
                .WithData(Deal(dealId, playerId2, shipTypeBattleship, price))
                .WithData(ShipsInDeal(shipTypeBattleship, playerId2, dealId)))
            .Calling(c => c.Buy(dealId))
            .ShouldHave()
            .TempData(d => d.ContainingEntryWithValue(string.Format(SuccessfulPurchase, 5, shipTypeBattleship)));

        [Fact]
        public void CreateDealWithNotEnoughShipsShouldReturnCorrectTempData()
            => MyController<AuctionController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId2))
                .WithData(PlayerWithCurrentSystem(playerId, systemId))
                .WithData(PlayerWithCurrentSystem(playerId2, systemId2))
                .WithData(System(playerId, systemId))
                .WithData(System(playerId2, systemId2))
                .WithData(Deal(dealId, playerId2, shipTypeBattleship, price))
                .WithData(FiveShipsOfPlayer(shipTypeBattleship, playerId2)))
            .Calling(c => c.Create(new CreateDealFormModel
            {
                Price = price,
                SellerId = playerId2,
                ShipCount = 10,
                ShipType = shipTypeBattleship
            }))
            .ShouldHave()
            .TempData(d => d.ContainingEntryWithValue(string.Format(NotEnoughShips, 5, shipTypeBattleship)));
    }
}

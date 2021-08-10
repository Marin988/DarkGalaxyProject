
using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Routing
{
    public class AuctionControllerTest
    {
        [Fact]
        public void AllShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Auction/All")
            .To<AuctionController>(c => c.All());

        [Fact]
        public void CreateShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Auction/Create")
            .To<AuctionController>(c => c.Create());
    }
}

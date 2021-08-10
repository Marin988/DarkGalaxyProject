
using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Routing
{
    public class AllianceControllerTest
    {
        [Fact]
        public void AllShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Alliance/All")
            .To<AllianceController>(c => c.All());

        [Fact]
        public void HomeShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Alliance/Home")
            .To<AllianceController>(c => c.Home());

        [Fact]
        public void MembersShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Alliance/Members?allianceId=1")
            .To<AllianceController>(c => c.Members("1"));

        [Fact]
        public void ChatShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Alliance/Chat?allianceId=1")
            .To<AllianceController>(c => c.Chat("1"));

        [Fact]
        public void NoAllianceHomeShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Alliance/NoAllianceHome")
            .To<AllianceController>(c => c.NoAllianceHome());

        [Fact]
        public void CreateShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Alliance/Create")
            .To<AllianceController>(c => c.Create());
    }
}

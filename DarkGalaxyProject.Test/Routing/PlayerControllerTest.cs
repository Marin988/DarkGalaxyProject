
using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Routing
{
    public class PlayerControllerTest
    {
        [Fact]
        public void RegisterShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/Register")
            .To<PlayerController>(c => c.Register());

        [Fact]
        public void LoginShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/Login")
            .To<PlayerController>(c => c.Login());

        [Fact]
        public void ResearchesShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/Researches?playerId=1")
            .To<PlayerController>(c => c.Researches("1"));

        [Fact]
        public void MessageShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/Message?messageId=1")
            .To<PlayerController>(c => c.Message("1"));

        [Fact]
        public void SendMessageShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/SendMessage")
            .To<PlayerController>(c => c.SendMessage());

        [Fact]
        public void MessagesShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/Messages?playerId=1")
            .To<PlayerController>(c => c.Messages("1"));

        [Fact]
        public void ProfileShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Player/Profile?playerId=1")
            .To<PlayerController>(c => c.Profile("1"));

        //[Fact]
        //public void StudyResearchShouldReturnCorrectRoute()
        //    => MyRouting
        //    .Configuration()
        //    .ShouldMap(request => request
        //            .WithPath("/Player/StudyResearch?researchId=1&systemId=1&playerId=1")
        //            .WithMethod(HttpMethod.Post))
        //    .To<PlayerController>(c => c.StudyResearch("1", "1", "1"));
    }
}

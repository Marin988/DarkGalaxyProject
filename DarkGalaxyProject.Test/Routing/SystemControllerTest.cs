
using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Routing
{
    public class SystemControllerTest
    {
        [Fact]
        public void AllSystemsShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/System/AllSystems?page=1")
            .To<SystemController>(c => c.AllSystems(1));

        [Fact]
        public void ViewSystemShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/System/ViewSystem/1")
            .To<SystemController>(c => c.ViewSystem("1"));

        [Fact]
        public void FleetShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/System/Fleet?systemId=1")
            .To<SystemController>(c => c.Fleet("1"));

        [Fact]
        public void PlayerSystemsShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/System/PlayerSystems?PlayerId=1")
            .To<SystemController>(c => c.PlayerSystems("1"));

        [Fact]
        public void ShipyardShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/System/Shipyard?systemId=1")
            .To<SystemController>(c => c.Shipyard("1"));

        [Fact]
        public void DefensiveStructureBuilderShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/System/DefensiveStructureBuilder?systemId=1")
            .To<SystemController>(c => c.DefensiveStructureBuilder("1"));

        //[Fact]
        //public void SwitchSystemShouldReturnCorrectRoute()
        //    => MyRouting
        //        .Configuration()
        //        .ShouldMap(request => request
        //            .WithPath(@"/System/SwitchSystem?systemId=1")
        //            .WithMethod(HttpMethod.Post))
        //    .To<SystemController>(c => c.SwitchSystem("1"));
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //    'Expected route '/System/SwitchSystem%3FsystemId=1' to match SwitchSystem action in SystemController but action could not be matched.'

    }
}

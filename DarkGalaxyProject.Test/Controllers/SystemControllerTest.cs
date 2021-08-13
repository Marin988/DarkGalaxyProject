
using DarkGalaxyProject.Controllers;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models.System;
using DarkGalaxyProject.Services.SystemServices;
using DarkGalaxyProject.Services.SystemServices.Models;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DarkGalaxyProject.Test.Controllers
{
    using static Data.Systems;

    public class SystemControllerTest
    {
        const string playerId = "playerId";
        const string systemId = "systemId";
        const string shipTypeBattleship = nameof(ShipType.BattleShip);
        const string defenceTypeSatelite = nameof(DefensiveStructureType.Satelite);
        const string missionTypeAttack = nameof(MissionType.Attack);
        const int destinationSystemPosition = 3;

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void AllSystemsShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers(
            int page)
           => MyController<SystemController>
            .Instance(instance => instance
                .WithData(PageSystems()))
            .Calling(c => c.AllSystems(page))
            .ShouldHave()
            .MemoryCache(cache => cache
                .ContainingEntryWithKey("AllSystems" + page))
            .AndAlso()
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<SystemPageViewModel>());

        [Fact]
        public void ViewSystemShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
            => MyController<SystemController>
            .Instance(controller => controller
                .WithData(OneSystem(systemId)))
            .Calling(c => c.ViewSystem(systemId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
                .WithModelOfType<SystemServiceModel>());

        [Fact]
        public void FleetShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
            => MyController<SystemController>
            .Instance(controller => controller
                .WithData(OneSystem(systemId))
                .WithData(Fleets())
                .WithData(Ships()))
            .Calling(c => c.Fleet(systemId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
                .WithModelOfType<FleetViewFormModel>());

        [Fact]
        public void PlayersSystemsShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<SystemController>
            .Instance(instance => instance
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(PageSystems()))
            .Calling(c => c.PlayerSystems(playerId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<IEnumerable<SystemServiceModel>>());

        [Fact]
        public void ShipyardSystemsShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<SystemController>
            .Instance(instance => instance
                .WithData(OneSystem(systemId)))
            .Calling(c => c.Shipyard(systemId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<IEnumerable<ShipBuilderServiceModel>>());

        [Fact]
        public void DefensiveStructureBuilderShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<SystemController>
            .Instance(instance => instance
                .WithData(OneSystem(systemId)))
            .Calling(c => c.DefensiveStructureBuilder(systemId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<IEnumerable<DefenceBuilderServiceModel>>());

        [Fact]
        public void SwitchSystemShouldBeForAuthorizedUsersAndRedirectToViewSystemAndSetCurrentSystem()
            => MyController<SystemController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId))
                .WithData(SystemOfPlayer(playerId, systemId)))
            .Calling(c => c.SwitchSystem(systemId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Player>(players => players
                    .Any(p =>
                        p.CurrentSystemId == systemId)))//should I test this here or in services?
            .AndAlso()
            .ShouldReturn()
            .Redirect($"ViewSystem/{systemId}");

        [Fact]
        public void StartBuildingShouldBeForAuthorizedUsersAndRedirectToShipyardAndSetBuildingFinishTime()
            => MyController<SystemController>
                .Instance(controller => controller
                .WithData(Player(playerId))
                .WithData(Research(playerId, shipTypeBattleship))
                .WithData(SystemOfPlayer(playerId, systemId))
                .WithData(ShipBuilder(systemId, shipTypeBattleship)))
            .Calling(c => c.StartBuilding(systemId, shipTypeBattleship, 1))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<ShipBuilder>(shipBuilders => shipBuilders
                    .Any(s =>
                        s.FinishedBuildingTime != null)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Shipyard?systemId={systemId}");

        [Fact]
        public void StartBuildingDefenceShouldBeForAuthorizedUsersAndRedirectToDefenceStructureBuilderAndSetBuildingFinishTime()
            => MyController<SystemController>
                .Instance(controller => controller
                .WithData(Player(playerId))
                .WithData(SystemOfPlayer(playerId, systemId))
                .WithData(DefenceBuilder(systemId, defenceTypeSatelite)))
            .Calling(c => c.StartBuildingDefence(systemId, defenceTypeSatelite, 1))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<DefenceBuilder>(defenceBuilder => defenceBuilder
                    .Any(d =>
                        d.FinishedBuildingTime != null)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"DefenceStructureBuilder?systemId={systemId}");

        [Fact]
        public void SendFleetOnAttackShouldBeForAuthorizedUsersAndRedirectToFleetAndSetArrivalTime()
            => MyController<SystemController>
                .Instance(controller => controller
                .WithData(OneSystem("SystemWithPosition3"))
                .WithData(Player(playerId))
                .WithData(SystemOfPlayer(playerId, systemId))
                .WithData(Fleet(systemId))
                .WithData(Ship(shipTypeBattleship, systemId, playerId)))
            .Calling(c => c.SendFleet(1, 0, 0, missionTypeAttack, destinationSystemPosition, systemId, 0))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Fleet>(fleet => fleet
                    .Any(f =>
                        f.ArrivalTime != null &&
                        f.DestinationSystemPoistion == destinationSystemPosition &&
                        f.MissionType == (MissionType)Enum.Parse(typeof(MissionType), missionTypeAttack))))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Fleet?systemId={systemId}");
    }
}

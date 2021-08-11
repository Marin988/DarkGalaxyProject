
using DarkGalaxyProject.Controllers;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.PlanetServices;
using DarkGalaxyProject.Services.PlanetServices.Models;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DarkGalaxyProject.Test.Controllers
{
    using static Data.PlanetData;
    using static Data.Systems;

    public class PlanetControllerTest
    {
        const string planetId = "planetId";
        const string factoryId = "factoryId";
        const string systemId = "systemId";
        const string playerId = "playerId";

        [Fact]
        public void ViewPlanetShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlanetController>
            .Instance(instance => instance
                .WithData(OneSystem(systemId))
                .WithData(Planet(planetId, systemId))
                .WithData(Factories(planetId, factoryId)))
            .Calling(c => c.ViewPlanet(planetId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<PlanetServiceModel>());

        [Fact]
        public void StartUpgradeShouldBeForAuthorizedUsersAndRedirectToViewPlanetAndIncreaseBuildingLevel()
            => MyController<PlanetController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId))
                .WithData(SystemOfPlayer(playerId, systemId))
                .WithData(Planet(planetId, systemId))
                .WithData(Factories(planetId, factoryId)))
            .Calling(c => c.StartUpgrade(factoryId, planetId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Factories>(factory => factory
                    .Any(f =>
                        f.UpgradeFinishTime != null)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"ViewPlanet?planetId={planetId}");
    }
}

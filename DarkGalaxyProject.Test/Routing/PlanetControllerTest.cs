
using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Routing
{
    public class PlanetControllerTest
    {
        [Fact]
        public void ViewPlanetShouldReturnCorrectRoute()
            => MyRouting
            .Configuration()
            .ShouldMap("/Planet/ViewPlanet?planetId=1")
            .To<PlanetController>(c => c.ViewPlanet("1"));
    }
}

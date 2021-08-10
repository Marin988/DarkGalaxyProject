
using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Routing
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexRoutShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/")
            .To<HomeController>(c => c.Index());

        [Fact]
        public void ErrorRouteShouldBeMapped()
            => MyRouting
            .Configuration()
            .ShouldMap("/Home/Error")
            .To<HomeController>(c => c.Error());
    }
}

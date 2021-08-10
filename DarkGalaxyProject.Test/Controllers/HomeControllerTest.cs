using DarkGalaxyProject.Controllers;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DarkGalaxyProject.Test.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Index())
            .ShouldReturn()
            .View();



        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
            .Instance()
            .Calling(c => c.Error())
            .ShouldReturn()
            .View();

    }
}

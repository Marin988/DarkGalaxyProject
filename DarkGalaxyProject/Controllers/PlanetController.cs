using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Services.PlanetServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DarkGalaxyProject.Controllers
{
    public class PlanetController : Controller
    {
        private readonly IPlanetService planets;
        private readonly UserManager<Player> userManager;

        public PlanetController(UserManager<Player> userManager, IPlanetService planets)
        {
            this.userManager = userManager;
            this.planets = planets;
        }

        [Authorize]
        public IActionResult ViewPlanet(string planetId)
        {
            var planet = planets.Planet(planetId);

            return View(planet);
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartUpgrade(string buildingId, string planetId)
        {
            var message = planets.StartUpgrade(buildingId, planetId, userManager.GetUserId(User));

            TempData["Message"] = message;

            return Json(new { redirectToUrl = Url.Action("ViewPlanet", "Planet", new { planetId = planetId }) });
        }

        [Authorize]
        [HttpPost]
        public IActionResult Terraform(string planetId)
        {
            var message = planets.Terraform(planetId, userManager.GetUserId(User));

            TempData["Message"] = message;

            return Redirect($"ViewPlanet?planetId={planetId}");
        }
    }
}

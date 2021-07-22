using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Models;
using DarkGalaxyProject.Models.Planet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Controllers
{
    public class PlanetController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public PlanetController(ApplicationDbContext data, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult ViewPlanet(string planetId)
        {
            var planet = data.Planets
                .Where(p => p.Id == planetId)
                .Select(p => new PlanetViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Position = p.Position,
                    Type = p.Type.ToString(),
                    Factories = new FactoriesViewModel
                    {
                        Id = p.Factories.Id,
                        Income = p.Factories.Income,
                        Level = p.Factories.Level,
                        UpgradeCost = p.Factories.UpgradeCost,
                        UpgradeTimeLength = p.Factories.UpgradeTimeLength
                    }
                })
                .First();

            return View(planet);
        }

        [Authorize]
        [HttpPost]
        public IActionResult LevelUp(string buildingId, string planetId)
        {

            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.Level += 1;

            data.SaveChanges();

            return Redirect($"ViewPlanet?planetId={planetId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetUpgradeTime(string buildingId, string planetId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.UpgradeTime = DateTime.Now.AddSeconds(factory.UpgradeTimeLength);

            data.SaveChanges();

            return Redirect($"ViewPlanet?planetId={planetId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult NullifyUpgradeTime(string buildingId, string planetId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.UpgradeTime = null;

            data.SaveChanges();

            return Redirect($"ViewPlanet?planetId={planetId}");
        }
    }
}

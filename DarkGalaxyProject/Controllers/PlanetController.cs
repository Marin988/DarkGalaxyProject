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
            var systemId = data.Planets.First(p => p.Id == planetId).SystemId;

            var playerId = data.Systems.First(s => s.Id == systemId).PlayerId;

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
                        UpgradeFinishTime = p.Factories.UpgradeFinishTime,
                        UpgradeStartTime = p.Factories.UpgradeStartTime,
                        UpgradeTimeLength = p.Factories.UpgradeTimeLength
                    },
                    PlayerId = playerId
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
        public IActionResult StartUpgrade(string buildingId, string planetId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            var planet = data.Planets.First(p => p.Id == planetId);

            var systemId = planet.SystemId;

            var system = data.Systems.First(s => s.Id == systemId);

            if(system.PlayerId != userManager.GetUserId(User))
            {
                Console.WriteLine("Hello?!");
                return BadRequest("You cannot upgrade other players' buildings!");
            }

            factory.UpgradeStartTime = DateTime.Now;

            data.SaveChanges();

            return Redirect($"ViewPlanet?planetId={planetId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SetUpgradeTime(string buildingId, string planetId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.UpgradeFinishTime = DateTime.Now.AddSeconds(factory.UpgradeTimeLength);

            data.SaveChanges();

            return Redirect($"ViewPlanet?planetId={planetId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult NullifyUpgradeTime(string buildingId, string planetId)
        {
            var factory = data.Factories.First(f => f.Id == buildingId);

            factory.UpgradeFinishTime = null;
            factory.UpgradeStartTime = null;

            data.SaveChanges();

            return Redirect($"ViewPlanet?planetId={planetId}");
        }
    }
}

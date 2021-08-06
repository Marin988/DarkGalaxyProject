using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models;
using DarkGalaxyProject.Models.Planet;
using DarkGalaxyProject.Models.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DarkGalaxyProject.BackgroundTasks;
using DarkGalaxyProject.Services.SystemServices;
using Microsoft.Extensions.Caching.Memory;

namespace DarkGalaxyProject.Controllers
{
    public class SystemController : Controller
    {
        private readonly UserManager<Player> userManager;
        private readonly ISystemService systems;
        private readonly IMemoryCache cache;

        public SystemController(UserManager<Player> userManager, ISystemService systems, IMemoryCache cache)
        {
            this.userManager = userManager;
            this.systems = systems;
            this.cache = cache;
        }

        [Authorize]
        public IActionResult ViewSystem(string Id)
        {
            var system = systems.System(Id);

            return View(system);
        }

        [Authorize]
        public IActionResult Fleet(string systemId)
        {
            var ships = systems.ShipsInSystem(systemId);

            bool colonise = ships.FirstOrDefault(s => s.Type == ShipType.Colonizer.ToString()) != null;//should this be in service? and do I even need this?

            var fleets = systems.FleetsInSystem(systemId);

            var hostSystemInfo = systems.HostSystemInfo(systemId);

            var fleet = new FleetViewFormModel
            {
                Ships = ships,
                Fleets = fleets,
                HostSystemInfo = hostSystemInfo
            };

            return View(fleet);
        }

        [Authorize]
        public IActionResult AllSystems(int page)
        {
            string allSystemsCacheKey = $"AllSystems{page}";

            var allSystems = cache.Get<List<SystemServiceModel>>(allSystemsCacheKey);

            if(allSystems == null)
            {
                allSystems = systems.AllSystems(page).ToList();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

                cache.Set(allSystemsCacheKey, allSystems, cacheOptions);
            }

            var pageSystems = new SystemPageViewModel
            {
                Systems = allSystems,
                Page = page
            };

            return View(pageSystems);
        }

        [Authorize]
        public IActionResult PlayerSystems(string PlayerId)
        {
            var playerSystems = systems.PlayerSystems(PlayerId);

            return View(playerSystems);
        }

        [Authorize]
        public IActionResult Shipyard(string systemId)
        {
            var shipBuilders = systems.ShipBuilders(systemId);

            return View(shipBuilders);
        }

        [Authorize]
        public IActionResult DefensiveStructureBuilder(string systemId)
        {
            var defenceBuilders = systems.DefenceBuilders(systemId);

            return View(defenceBuilders);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SwitchSystem(string systemId)
        {
            systems.SwitchSystem(systemId, userManager.GetUserId(User));

            return Redirect($"ViewSystem/{systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Colonize(string systemId)
        {
            systems.Colonize(systemId, userManager.GetUserId(User));

            return Redirect($"PlayerSystems?PlayerId={userManager.GetUserId(User)}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendFleet(int battleShipCount, int colonizerCount, int transportShipCount, string missionType, int cargo, int destinationSystemPosition, string systemId)
        {
            var message = systems.SendFleet(battleShipCount, colonizerCount, transportShipCount, missionType, cargo, destinationSystemPosition, systemId);

            TempData["Message"] = message;

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetReturn(string systemId)
        {
            systems.FleetReturn(systemId);

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetBoarding(string systemId)
        {
            systems.FleetBoarding(systemId);

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartBuilding(string systemId, string shipType, int count)
        {
            var message = systems.StartBuildingShip(systemId, shipType, count);

            TempData["Message"] = message;

            return Redirect($"Shipyard?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartBuildingDefence(string systemId, string defenceType, int count)
        {
            var message = systems.StartBuildingDefence(systemId, defenceType, count);

            TempData["Message"] = message;

            return Redirect($"DefenceStructureBuilder?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuildShip(string systemId, string shipType)
        {
            systems.BuildShip(systemId, shipType, userManager.GetUserId(User));

            return Redirect($"Shipyard?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuildDefence(string systemId, string defenceType)
        {
            systems.BuildDefence(systemId, defenceType);

            return Redirect($"DefenceStructureBuilder?systemId={systemId}");
        }

    }
}

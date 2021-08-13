using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Services.PartialServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Controllers
{
    public class PartialController : Controller
    {
        private readonly UserManager<Player> userManager;
        private readonly IPartialService partialService;

        public PartialController(UserManager<Player> userManager, IPartialService partialService)
        {
            this.userManager = userManager;
            this.partialService = partialService;
        }

        [Authorize]
        public IActionResult SystemResources(string playerId)
        {
            var systemResources = partialService.SystemResources(playerId);

            return PartialView(systemResources);
        }

        [Authorize]
        public IActionResult PlayerSystemsOverview(string playerId)
        {
            var playerSystems = partialService.PlayerSystems(playerId);

            return PartialView(playerSystems);
        }
    }
}

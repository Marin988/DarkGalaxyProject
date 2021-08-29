using DarkGalaxyProject.Areas.Partials.Services;
using DarkGalaxyProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DarkGalaxyProject.Areas.Partials.Controllers
{
    [Area(PartialConstants.AreaName)]
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

        [Authorize]
        public IActionResult GetUnseenMessagesCount(string playerId)
        {
            var playerUnseenMessagesCount = partialService.PlayerUnseenMessagesCount(playerId);

            return Ok(JsonConvert.SerializeObject(playerUnseenMessagesCount));
        }
    }
}

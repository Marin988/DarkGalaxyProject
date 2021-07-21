using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Models;
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
        [HttpPost]
        public IActionResult LevelUp(string buildingId, string systemId, string planetType)
        {

            //var factory = data.Factories.First(f => f.Id == buildingId);
            

            data.SaveChanges();

            return Redirect($"{planetType}?systemId={systemId}");
        }
    }
}

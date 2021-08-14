using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Models;
using DarkGalaxyProject.Models.Player;
using DarkGalaxyProject.Services.PlayerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService players;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public PlayerController(UserManager<Player> userManager, SignInManager<Player> signInManager, IPlayerService players)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.players = players;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult AllPlayers()
        {
            var allPlayers = players.AllPlayers();

            return View(allPlayers);
        }

        [Authorize]
        public IActionResult Researches(string playerId)
        {
            var researches = players.Researches(playerId);

            return View(researches);
        }

        [Authorize]
        public IActionResult Message(string messageId)
        {
            var message = players.Message(messageId);

            return View(message);
        }

        [Authorize]
        public IActionResult SendMessage()
        {
            return View();
        }

        [Authorize]
        public IActionResult Messages(string playerId)
        {
            var messages = players.PlayerMessages(playerId);

            return View(messages);
        }

        [Authorize]
        public IActionResult Profile(string playerId)
        {
            var player = players.Profile(playerId);

            return View(player);
        }

        [Authorize]
        [HttpPost]
        public IActionResult StudyResearch(string researchId, string systemId, string playerId)
        {
            var message = players.StudyResearch(researchId, systemId);

            TempData["Message"] = message;

            return Redirect($"Researches?playerId={playerId}");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginFormModel user)
        {
            var loggedInUser = await userManager.FindByEmailAsync(user.Email);

            if(user == null)
            {
                return InvalidCredentials(user);
            }

            var passwordIsValid = await userManager.CheckPasswordAsync(loggedInUser, user.Password);

            if (!passwordIsValid)
            {
                return InvalidCredentials(user);
            }

            await signInManager.SignInAsync(loggedInUser, true);

            return RedirectToAction("Index", "Home");
        }

        private IActionResult InvalidCredentials(LoginFormModel user)
        {
            const string invalidCredentialsMessage = "Credentials invalid!";

            ModelState.AddModelError(string.Empty, invalidCredentialsMessage);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var systemForUser = players.StartingSystem();

            var registeredUser = new Player()
            {
                Email = user.Email,
                UserName = user.Username,
                CurrentSystemId = systemForUser.Id,
                Systems = new List<Data.Models.System>() { systemForUser },
            };

            var result = await userManager.CreateAsync(registeredUser, user.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View(user);
            }

            players.PlayerResearches(registeredUser.Id);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage(MessageFormModel message)
        {
            var Errormessage = players.SendMessage(message.Content, message.ReceiverName, message.SenderId, message.Title);

            TempData["Message"] = Errormessage;

            if(Errormessage != null)
            {
                return Redirect($"SendMessage?playerId={userManager.GetUserId(User)}");
            }

            return Redirect($"Messages?playerId={userManager.GetUserId(User)}");
        }
    }
}

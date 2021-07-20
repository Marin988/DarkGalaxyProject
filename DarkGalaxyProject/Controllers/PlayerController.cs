using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Models;
using DarkGalaxyProject.Models.Player;
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
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public PlayerController(ApplicationDbContext data, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult Message(string messageId)
        {
            var message = data.Messages
                .Where(m => m.Id == messageId)
                .Select(m => new MessageViewModel
                {
                    Content = m.Content,
                    SenderId = m.SenderId,
                    SenderName = m.Sender.UserName,
                    Title = m.Title,
                    Time = m.TimeOfSending
                })
                .First();

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
            var messages = data.Messages
                .Where(m => m.ReceiverId == playerId || m.SenderId == playerId)
                .Select(m => new MessageListingViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    ReceiverId = m.ReceiverId == playerId ? m.SenderId : m.ReceiverId,
                    ReceiverName = m.ReceiverId == playerId ? m.Sender.UserName : m.Receiver.UserName,
                    Time = m.TimeOfSending
                })
                .ToList()
                .OrderBy(m => m.Time)
                .ToList();

            return View(messages);
        }

        [Authorize]
        public IActionResult Profile(string playerId)
        {
           var player = data.Players.Where(p => p.Id == playerId)
                .Select(p => new ProfileViewModel
                {
                    UserName = p.NormalizedUserName,
                    AllianceName = p.Alliance.Name,
                    Systems = p.Systems.Count()
                })
                .FirstOrDefault();

            return View(player);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendMessage(MessageFormModel message)
        {
            data.Messages.Add(new Message
            {
                Content = message.Content,
                ReceiverId = data.Players.First(p => p.UserName == message.ReceiverName).Id,
                SenderId = message.SenderId,
                Title = message.Title
            });

            data.SaveChanges();

            return Redirect($"Messages?playerId={userManager.GetUserId(User)}");
        }
    }
}

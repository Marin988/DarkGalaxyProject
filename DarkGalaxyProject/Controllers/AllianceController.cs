using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Models.Alliance;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Controllers
{
    public class AllianceController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public AllianceController(ApplicationDbContext data, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var alliances = data.Alliances
                .Select(a => new AllianceViewModel
                {
                    Id = a.Id,
                    Leader = a.Leader.UserName,
                    MembersCount = a.Members.Count(),
                    Name = a.Name,
                })
                .ToList();

            return View(alliances);
        }

        [Authorize]
        public IActionResult Home()
        {
            var allianceId = data.Players.First(u => u.Id == userManager.GetUserId(User)).AllianceId;

            if (allianceId == null)
            {
                return Redirect("NoAllianceHome");
            }

            var alliance = data.Alliances
                .Where(a => a.Id == allianceId)
                .Select(a => new AllianceViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Leader = a.Leader == null ? null : a.Leader.UserName,
                    MembersCount = a.Members.Count()
                })
                .First();

            return View(alliance);
        }

        [Authorize]
        public IActionResult Members(string allianceId)
        {
            var members = data.Players
                .Where(p => p.AllianceId == allianceId)
                .Select(p => new MemberViewModel
                {
                    Id = p.Id,
                    Systems = p.Systems.Count(),
                    UserName = p.UserName
                })
                .ToList();

            var candidates = data.Players
                .Where(p => p.AllianceCandidateId == allianceId)
                .Select(p => new MemberViewModel
                {
                    Id = p.Id,
                    Systems = p.Systems.Count(),
                    UserName = p.UserName
                })
                .ToList();

            var membersAndCandidates = new MembersCandidatesViewModel
            {
                Candidates = candidates,
                Members = members,
                allianceId = allianceId
            };

            return View(membersAndCandidates);
        }

        [Authorize]
        public IActionResult Chat(string allianceId)
        {
            var messages = data.ChatMessages
                .Where(c => c.AllianceId == allianceId)
                .Select(c => new ChatViewMessageModel
                {
                    AllianceId = c.AllianceId,
                    Content = c.Content,
                    Sender = c.Player.UserName,
                    SenderId = c.PlayerId,
                    TimeOfSending = c.TimeOfSending
                })
                .ToList();

            var chat = new ChatViewModel
            {
                Messages = messages,
                AllianceId = allianceId,
                SenderId = userManager.GetUserId(User),
                Sender = userManager.GetUserName(User)
            };

            return View(chat);
        }

        [Authorize]
        public IActionResult NoAllianceHome()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Send(ChatViewModel message)
        {
            data.ChatMessages.Add(new ChatMessage
            {
                AllianceId = message.AllianceId,
                Content = message.Content,
                PlayerId = message.SenderId
            });

            data.SaveChanges();

            return Redirect($"Chat?allianceId={message.AllianceId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateViewModel allianceModel)
        {
            var player = data.Players.First(p => p.Id == userManager.GetUserId(User));

            var alliance = new Alliance(allianceModel.Name);

            alliance.Members.ToList().Add(player);

            data.Alliances.Add(alliance);

            data.SaveChanges();

            player.AllianceId = alliance.Id;

            alliance.LeaderId = userManager.GetUserId(User);
            player.AllianceLeaderId = alliance.Id;

            data.SaveChanges();

            return Redirect("Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Leave(string playerId)
        {
            var player = data.Players.First(p => p.Id == playerId);

            player.AllianceId = null;

            data.SaveChanges();

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AcceptCandidate(string allianceId, string candidateId)
        {
            var candidate = data.Players.First(p => p.Id == candidateId);

            candidate.AllianceCandidateId = null;

            candidate.AllianceId = allianceId;

            data.SaveChanges();

            return Redirect($"Members?allianceId={allianceId}");
        }


        [Authorize]
        [HttpPost]
        public IActionResult Apply(string allianceId)
        {
            var candidate = data.Players.First(p => p.Id == userManager.GetUserId(User));

            candidate.AllianceCandidateId = allianceId;

            data.SaveChanges();

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult PromoteToLeader(string allianceId, string playerId) //SqlException: Cannot insert duplicate key row in object ...
        {
            var player = data.Players.First(p => p.Id == userManager.GetUserId(User));

            player.AllianceLeaderId = allianceId;

            data.SaveChanges();

            return Redirect($"Members?allianceId={allianceId}");
        }

    }
}

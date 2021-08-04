using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Models.Alliance;
using DarkGalaxyProject.Services.AllianceServices;
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
        private readonly IAllianceService alliances;
        private readonly UserManager<Player> userManager;

        public AllianceController(UserManager<Player> userManager, IAllianceService alliances)
        {
            this.userManager = userManager;
            this.alliances = alliances;
        }

        [Authorize]
        public IActionResult All()
        {
            var AllAlliances = alliances.All();

            return View(AllAlliances);
        }

        [Authorize]
        public IActionResult Home()
        {
            var playerId = userManager.GetUserId(User);

            if (!alliances.IsInAlliance(playerId))
            {
                return Redirect("NoAllianceHome");
            }

            var alliance = alliances.Home(playerId);

            return View(alliance);
        }

        [Authorize]
        public IActionResult Members(string allianceId)
        {
            var members = alliances.Members(allianceId);

            var candidates = alliances.Candidates(allianceId);

            var membersAndCandidates = new MembersCandidatesViewModel
            {
                Members = members,
                Candidates = candidates,
                AllianceId = allianceId
            };

            return View(membersAndCandidates);
        }

        [Authorize]
        public IActionResult Chat(string allianceId)
        {
            var messages = alliances.ChatMessages(allianceId);

            var chat = new ChatViewModel
            {
                Messages = messages,
                AllianceId = allianceId,
                PlayerId = userManager.GetUserId(User),
                Player = userManager.GetUserName(User)
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
            alliances.Send(message.AllianceId, message.Content, message.PlayerId); 

            return Redirect($"Chat?allianceId={message.AllianceId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateViewModel allianceModel)
        {
            alliances.Create(userManager.GetUserId(User), allianceModel.Name);

            return Redirect("Home");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Leave(string playerId)
        {
            alliances.Leave(playerId);

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AcceptCandidate(string allianceId, string candidateId)
        {
            alliances.AcceptCandidate(allianceId, candidateId);

            return Redirect($"Members?allianceId={allianceId}");
        }


        [Authorize]
        [HttpPost]
        public IActionResult Apply(string allianceId)
        {
            alliances.Apply(allianceId, userManager.GetUserId(User));

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult PromoteToLeader(string allianceId, string playerId) //SqlException: Cannot insert duplicate key row in object ...
        {
            alliances.PromoteToLeader(allianceId, playerId);

            return Redirect($"Members?allianceId={allianceId}");
        }

    }
}

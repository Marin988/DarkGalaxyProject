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
            var playerId = userManager.GetUserId(User);
            var AllAlliances = alliances.All();
            var isInAlliance = alliances.IsInAlliance(playerId);

            var AllAlliancesViewModel = new AllAlliancesViewModel
            {
                Alliances = AllAlliances,
                IsInAlliance = isInAlliance
            };

            return View(AllAlliancesViewModel);
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

            var leaderId = alliances.LeaderId(allianceId);

            var membersAndCandidates = new MembersCandidatesViewModel
            {
                Members = members,
                Candidates = candidates,
                AllianceId = allianceId,
                AllianceLeaderId = leaderId
            };

            return View(membersAndCandidates);
        }

        [Authorize]
        public IActionResult Chat(string allianceId)
        {
            var messages = alliances.ChatMessages(allianceId);

            var chat = new ChatFormViewModel
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
        public IActionResult Send(ChatFormViewModel message)
        {
            var errorMessage = alliances.Send(message.AllianceId, message.Content, message.PlayerId);

            TempData["Message"] = errorMessage;
            //TempData.Keep("Message");
            //TempData.Peek("Message");

            return Redirect($"Chat?allianceId={message.AllianceId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateFormModel allianceModel)
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
            var message = alliances.AcceptCandidate(allianceId, candidateId, userManager.GetUserId(User));

            TempData["Message"] = message;

            return Redirect($"Members?allianceId={allianceId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult RejectCandidate(string allianceId, string candidateId)
        {
            var message = alliances.RejectCandidate(allianceId, candidateId, userManager.GetUserId(User));

            TempData["Message"] = message;

            return Redirect($"Members?allianceId={allianceId}");
        }


        [Authorize]
        [HttpPost]
        public IActionResult Apply(string allianceId)
        {
            var message = alliances.Apply(allianceId, userManager.GetUserId(User));

            TempData["Message"] = message;

            return Redirect("All");
        }

        [Authorize]
        [HttpPost]
        public IActionResult PromoteToLeader(string allianceId, string playerId) //SqlException: Cannot insert duplicate key row in object ...
        {
            var message = alliances.PromoteToLeader(allianceId, playerId, userManager.GetUserId(User));

            TempData["Message"] = message;

            return Redirect($"Members?allianceId={allianceId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeDescription(string allianceId, string description)
        {
            var message = alliances.ChangeDescription(allianceId, description);

            TempData["Message"] = message;

            return Json(new { redirectToUrl = Url.Action("Home", "Alliance") } );
        }
    }
}

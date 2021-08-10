
using DarkGalaxyProject.Controllers;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Models.Alliance;
using DarkGalaxyProject.Services.AllianceServices;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DarkGalaxyProject.Test.Controllers
{
    using static Data.AllianceData;
    using static Data.Players;

    public class AllianceControllerTest
    {
        const string allianceId = "allianceId";
        const string playerId = "playerId";
        const string content = "content";
        const string allianceName = "allianceName";
        const string candidateId = "candidateId";

        [Fact]
        public void AllShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<AllianceController>
            .Instance(instance => instance
                .WithData(FiveAlliances(allianceId)))
            .Calling(c => c.All())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<IEnumerable<AllianceServiceModel>>());

        [Fact]
        public void HomeShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<AllianceController>
            .Instance(instance => instance
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(AllianceLeader(candidateId, allianceId))
                .WithData(AllianceMember(playerId, allianceId))
                .WithData(Alliance(allianceId)))
            .Calling(c => c.Home())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<AllianceServiceModel>());

        [Fact]
        public void MembersShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<AllianceController>
            .Instance(instance => instance
                .WithData(FiveAlliances(allianceId).First())
                .WithData(Members(allianceId + 1))
                .WithData(Candidates(allianceId + 1)))
            .Calling(c => c.Members(allianceId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<MembersCandidatesViewModel>());

        [Fact]
        public void ChatShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<AllianceController>
            .Instance(instance => instance
                .WithData(FiveAlliances(allianceId).First())
                .WithData(ChatMessages(allianceId + 1)))
            .Calling(c => c.Chat(allianceId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<ChatFormViewModel>());

        [Fact]
        public void NoAllianceHomeShouldReturnCorrectViewAndShouldBeForAuthorizedUsers()
           => MyController<AllianceController>
            .Instance()
            .Calling(c => c.NoAllianceHome())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View();

        [Fact]
        public void CreateShouldReturnCorrectViewAndShouldBeForAuthorizedUsers()
           => MyController<AllianceController>
            .Instance()
            .Calling(c => c.Create())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View();

        [Fact]
        public void SendShouldBeForAuthorizedUsersAndCreateChatMessageAndRedirectToChat()//I am not testing the payment
            => MyController<AllianceController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(AllianceMember(playerId, allianceId))
                .WithData(Alliance(allianceId)))
            .Calling(c => c.Send(new ChatFormViewModel
            {
                AllianceId = allianceId,
                Content = content,
                PlayerId = playerId
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<ChatMessage>(messages => messages
                    .Any(m => m.AllianceId == allianceId &&
                        m.Content == content &&
                        m.PlayerId == playerId)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Chat?allianceId={allianceId}");

        [Fact]
        public void CreateShouldBeForAuthorizedUsersAndCreateAllianceAndRedirectToAllianceHome()//I am not testing the payment
            => MyController<AllianceController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId)))
            .Calling(c => c.Create(new CreateFormModel
            {
                Name = allianceName
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Alliance>(alliances => alliances
                    .Any(a => a.Name == allianceName &&
                        a.LeaderId == playerId)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("Home");

        [Fact]
        public void LeaveShouldBeForAuthorizedUsersAndLetPlayerLeaveAllianceAndRedirectToAll()//I am not testing the payment
            => MyController<AllianceController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(AllianceMember(playerId, allianceId))
                .WithData(Alliance(allianceId)))
            .Calling(c => c.Leave(playerId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Player>(players => players
                    .Any(p => p.AllianceId == null)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("All");

        [Fact]
        public void AcceptCandidateShouldBeForAuthorizedUsersAndRedirectToAllianceMembers()//I am not testing the payment
            => MyController<AllianceController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(AllianceLeader(playerId, allianceId))
                .WithData(Player(candidateId))
                .WithData(Alliance(allianceId)))
            .Calling(c => c.AcceptCandidate(allianceId, candidateId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Player>(players => players
                    .Any(p => p.AllianceId == allianceId &&
                        p.Id == candidateId)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Members?allianceId={allianceId}");

        [Fact]
        public void ApplyShouldBeForAuthorizedUsersAndRedirectToAllAndAddPlayerToCandidates()//I am not testing the payment
            => MyController<AllianceController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId))
                .WithData(Alliance(allianceId)))
            .Calling(c => c.Apply(allianceId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Player>(players => players
                    .Any(p => p.AllianceCandidateId == allianceId &&
                        p.Id == playerId)))
            .AndAlso()
            .ShouldReturn()
            .Redirect("All");

        [Fact]
        public void PromoteToLeaderShouldBeForAuthorizedUsersAndRedirectToAllianceMembersAndAssignNewLeader()//I am not testing the payment
            => MyController<AllianceController>
                .Instance(controller => controller
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(AllianceLeader(playerId, allianceId))
                .WithData(AllianceMember(candidateId, allianceId))
                .WithData(Alliance(allianceId)))
            .Calling(c => c.PromoteToLeader(allianceId, candidateId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Player>(players => players
                    .Any(p => p.AllianceLeaderId == allianceId &&
                        p.Id == candidateId)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Members?allianceId={allianceId}");
    }
}

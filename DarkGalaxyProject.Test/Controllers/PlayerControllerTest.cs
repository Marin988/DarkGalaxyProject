
using DarkGalaxyProject.Controllers;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Models.Player;
using DarkGalaxyProject.Services.PlayerServices;
using MyTested.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DarkGalaxyProject.Test.Controllers
{
    using static Data.Players;
    //using static Data.AllianceData;

    public class PlayerControllerTest
    {
        const string playerId = "playerId";
        const string messageId = "messageId";
        const string allianceId = "allianceId";
        const string researchId = "researchId";
        const string systemId = "systemId";
        const string playerName = "Rin";
        const string messageTitle = "Title";
        const string messageContent = "Content";
        const string email = "niram323@gmail.com";
        const string password = "aA2!jkl";

        [Fact]
        public void RegisterShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance()
            .Calling(c => c.Register())
            .ShouldReturn()
           .View();

        [Fact]
        public void LoginShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance()
            .Calling(c => c.Login())
            .ShouldReturn()
           .View();

        [Fact]
        public void ResearchesShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance(instance => instance
                .WithData(Player(playerId))
                .WithData(Research(playerId, researchId)))
            .Calling(c => c.Researches(playerId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<ResearchListServiceModel>());

        [Fact]
        public void MessageShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance(instance => instance
                .WithData(Player(playerId))
                .WithData(Message(messageId, playerId)))
            .Calling(c => c.Message(messageId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<MessageServiceModel>());

        [Fact]
        public void SendMessageShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance()
            .Calling(c => c.SendMessage())
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View();

        [Fact]
        public void MessagesShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance(instance => instance
                .WithData(Player(playerId))
                .WithData(Messages(playerId)))
            .Calling(c => c.Messages(playerId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<IEnumerable<MessageListingServiceModel>>());

        [Fact]
        public void ProfileShouldReturnCorrectViewWithModelAndDataAndShouldBeForAuthorizedUsers()
           => MyController<PlayerController>
            .Instance(instance => instance
                .WithData(Alliance(allianceId))
                .WithData(PlayerInAlliance(playerId, allianceId))
                .WithData(System(playerId, systemId)))
            .Calling(c => c.Profile(playerId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForAuthorizedRequests())
            .AndAlso()
            .ShouldReturn()
           .View(view => view
               .WithModelOfType<ProfileServiceModel>());

        [Fact]
        public void StudyResearchShouldBeForAuthorizedUsersAndRedirectToResearchesAndSetIsLearnedToTrue()
            => MyController<PlayerController>
                .Instance(controller => controller
                .WithData(Player(playerId))
                .WithData(System(playerId, systemId))
                .WithData(Research(playerId, researchId)))
            .Calling(c => c.StudyResearch(researchId, systemId, playerId))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<ResearchTree>(research => research
                    .Any(r =>
                        r.IsLearned == true )))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Researches?playerId={playerId}");

        //test if researches and a starting system are added to the player upon registering
        [Fact]
        public void RegisteredPlayerStartsWithOneSystemAndListOfAllResearchesWhichAreNotLearned()
            => MyController<PlayerController>
                .Instance(controller => controller
                .WithData(StartingSystem(systemId)))
            .Calling(c => c.Register(new RegisterFormModel
            {
                Username = playerName,
                Email = email,
                Password = password
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post))
            .ValidModelState()
            .Data(data => data
                .WithSet<Player>(player => player
                    .Any(p =>
                        p.Systems.Count() == 1 &&
                        p.ResearcheTree.Count() == ResearchTypeCount() &&
                        p.ResearcheTree.All(r => !r.IsLearned)
                        )))
            .AndAlso()
            .ShouldReturn()
            .RedirectToAction("Index", "Home");

        [Fact]
        public void SendMessageShouldBeForAuthorizedUsersAndRedirectToMessagesAndAddNewMessage()
            => MyController<PlayerController>
                .Instance(controller => controller
                .WithData(MessageReciever(playerName))
                .WithUser(user => user.WithIdentifier(playerId))
                .WithData(Player(playerId)))
            .Calling(c => c.SendMessage(new MessageFormModel
            {
                Content = messageContent,
                ReceiverName = playerName,
                SenderId = playerId,
                Title = messageTitle
            }))
            .ShouldHave()
            .ActionAttributes(attributes => attributes
                .RestrictingForHttpMethod(HttpMethod.Post)
                .RestrictingForAuthorizedRequests())
            .ValidModelState()
            .Data(data => data
                .WithSet<Message>(message => message
                    .Any(m =>
                        m.Content == messageContent &&
                        m.Receiver.UserName == playerName &&
                        m.SenderId == playerId &&
                        m.Title == messageTitle)))
            .AndAlso()
            .ShouldReturn()
            .Redirect($"Messages?playerId={playerId}");
    }
}

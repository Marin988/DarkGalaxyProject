using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject
{
    public class GlobalConstants
    {
        public class AllianceConstants
        {
            public const string OnlyLeaderCanAcceptMembers = "Only the leader can accept members!";
            public const string OnlyLeaderCanRejectMembers = "Only the leader can reject members!";
            public const string ApplySuccess = "You have applied to {0}";
            public const string OnlyLeaderCanPromoteMembers = "Only the leader can promote other members!";
            public const string YouCanOnlyPromoteMembersOfThisAlliance = "You can only promote members of this alliance!";
            public const string YouAreAlreadyTheLeaderOfThisAlliance = "You are already the leader of this alliance!";
            public const string MessageShouldNotBeNullOrEmpty = "Message should contain at least one letter or digit!";
            public const string OnlyMembersOfThisAllianceCanUseTheChat = "Only members of this alliance can participate in this chat!";
            public const string DescriptionIsNotTheCorrectLength = "Description should be between 6 and 120 symbols!";
        }

        public class AuctionConstants
        {
            public const string NotEnoughCoin = "You need {0} {1}, but only have {2}!";
            public const string SuccessfulPurchase = "Successfully purchased {0} ships of type {1}!";
            public const string NotEnoughShips = "You only have {0} available ships of type {1}.";
        }

        public class PlanetConstants
        {
            public const string BuildingAlreadyUpgrading = "This building is already in the process of upgrading!";
            public const string NotEnoughSpace = "You don't have enough space on this planet to build that!";
            public const string NotEnoughResource = "You don't have enough {0}.";
            public const string CanOnlyUpgradeBuildingsInOwnSystem = "You can only upgprade buildings built in your systems.";
            public const string UpgradeHasStarted = "You have started an upgrade for {0} {1} and {2} {3}";
            public const string PlanetAlreadyTerraformed = "This planet has already been terraformed.";
            public const string TerraformingResearchNotLearned = "You have not yet learned the research required for terraforming.";
            public const string SuccessfullyTerraformedPlanet = "You have successfully terraformed this planet in exchange for {0} {1}.";
        }

        public class PlayerConstants
        {
            public const string NoPlayerWithSuchName = "There is no player with such name.";
            public const string CannotSendMessagesToOneself = "You can't send messages to yourself.";
            public const string AlreadyLearnedResearch = "You have already done this research!";
            public const string NotEnoughResource = "You don't have enough {0}.";
        }
    }
}

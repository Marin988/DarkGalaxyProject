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

        public class SystemConstants
        {
            public const string NoAvailableFleets = "You don't have available fleets";
            public const string FleetShouldHaveAtLeastOneShip = "You cannot send a fleet of 0 ships";
            public const string NoHostileMissionsOnOwnSystems = "You cannot attack, spy or colonize your own systems.";
            public const string CannotFindSystemWithThisPosition = "A system with position {0} doesn't exist!";
            public const string NoShipsOnSystemSuitableForThisMission = "You don't have any ships, suitable for {0} mission on this system!";
            public const string CannotSendShipsToTheSameSystem = "You cannot send ships to their current location.";
            public const string DeployAndTransportTargetSystemsShouldBelongToPlayer = "You cannot deploy ships and transport resources to systems not belonging to a player.";
            public const string CannotColoniseSystemsBelongingToOtherPlayers = "You cannot colonise systems belonging to a player.";
            public const string SpyAndAttackMissionsCannotTransport = "You cannot transport resources on spy or attack mission.";
            public const string NeedsEspionageToSpy = "You can't spy without espionages.";
            public const string OnlyEspionagesCanBeSentOnSpyMission = "You can only send epsionages to a spy mission. If you have one, it will automatically be added to your fleet.";
            public const string InsufficientFuel = "You need {0} fuel for this mission, but only have {1}.";
            public const string FleetSentOnMission = "Successfully sent {0} ships on mission {1} to system {2}.";
            public const string NotEnoughShipsOfType = "You don't have any {0}s.";
            public const string ValidCount = "Count has to be more than 0.";
            public const string CanOnlyBuildOnOwnSystems = "Only the player, whom this system belongs to can build here.";
            public const string DefenceIsAlreadyBuilding = "You are already building defences.";
            public const string ShipIsAlreadyBuilding = "You are already building ships.";
            public const string MustLearnResearchBeforeBuilding = "You must learn {0} to be able to build {1}.";
            public const string InsufficientResourcesForBuilding = "You need {0} {1} to build {2} {3}s, but only have {4}.";
            public const string StartedBuilding = "You have started building {0} {1}s for {2} {3}.";
            public const string NeedResearchForBuildingShip = "You have to research {0} before building ship of type {1}.";
            public const string AlreadyHaveFiveFleets = "You already have the maximum of 5 fleets.";
            public const string ResearchNotLearned = "You have not yet learned the {0} research.";
            public const string InsufficientResources = "You need {0} {1}, but you only have {2}.";
            public const string NewFleetAdded = "You have successfully added a new fleet.";
        }
    }
}

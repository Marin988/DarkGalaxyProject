using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Test.Data
{
    public class Players
    {
        public static ResearchTree Research(string playerId, string researchId)
            => new ResearchTree(playerId, ResearchType.BattleShip, null, null, 0) { Id = researchId };

        public static Player Player(string playerId)
            => new Player { Id = playerId };

        public static Player PlayerWithCurrentSystem(string playerId, string currentSystemId)
            => new Player { Id = playerId, CurrentSystemId = currentSystemId };


        public static Message Message(string messageId, string playerId)
            => new Message { Id = messageId, SenderId = playerId };

        public static IEnumerable<Message> Messages(string playerId)
            => Enumerable.Range(0, 5).Select(m => new Message
            {
                SenderId = playerId
            });

        public static DarkGalaxyProject.Data.Models.System System(string playerId, string systemId)
            => new DarkGalaxyProject.Data.Models.System { Id = systemId, PlayerId = playerId, Resources = new List<Resource>() { MilkyCoin(systemId) } };

        public static Resource MilkyCoin(string systemId)
            => new Resource { Quantity = 5000, SystemId = systemId, Type = ResourceType.MilkyCoin };

        public static Alliance Alliance(string allianceId)
            => new Alliance("Demolishers") { Id = allianceId };

        public static Player PlayerInAlliance(string playerId, string allianceId)
            => new Player { Id = playerId, AllianceId = allianceId };

        public static Player MessageReciever(string playerName)
            => new Player { UserName = playerName };

        public static DarkGalaxyProject.Data.Models.System StartingSystem(string systemId)
            => new DarkGalaxyProject.Data.Models.System { Id = systemId, Resources = new List<Resource>() { MilkyCoin(systemId) } };

        public static int ResearchTypeCount()
            => (Enum.GetValues(typeof(ResearchType))).Length;
    }
}

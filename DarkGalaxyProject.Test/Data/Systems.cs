using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models.System;
using DarkGalaxyProject.Services.SystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Test.Data
{
    public static class Systems
    {
        public static IEnumerable<DarkGalaxyProject.Data.Models.System> PageSystems()
            => Enumerable.Range(0, 20).Select(s => new DarkGalaxyProject.Data.Models.System
            {
                Type = SystemType.Large
            });

        public static SystemPageViewModel PageViewModel(int i)
            => new SystemPageViewModel
            {
                //Systems = PageSystems(),
                Page = i
            };

        public static DarkGalaxyProject.Data.Models.System SystemWithPosition3(string systemId)
            => new DarkGalaxyProject.Data.Models.System
            {
                Id = systemId,
                Type = SystemType.Large,
                Position = 3
            };

        public static IEnumerable<Ship> Ships()
            => Enumerable.Range(0, 5).Select(s => new Ship(ShipType.BattleShip, "1", "1", 0, 0, 0, 0, 0));

        public static IEnumerable<Fleet> Fleets()
            => Enumerable.Range(0, 5).Select(f => new Fleet("1"));

        public static Player Player(string playerId)
            => new Player { Id = playerId };

        public static DarkGalaxyProject.Data.Models.System SystemOfPlayer(string playerId, string systemId)
            => new DarkGalaxyProject.Data.Models.System { Id = systemId, PlayerId = playerId, Resources = new List<Resource>() { MilkyCoin(systemId), Energy(systemId), Fuel(systemId) } };

        public static ResearchTree Research(string playerId, string shiptype)
            => new ResearchTree(playerId, (ResearchType)Enum.Parse(typeof(ResearchType), shiptype), null, null, 0) { IsLearned = true };

        public static ShipBuilder ShipBuilder(string systemId, string shiptype)
            => new ShipBuilder { SystemId = systemId, ShipType = (ShipType)Enum.Parse(typeof(ShipType), shiptype) };

        public static Resource MilkyCoin(string systemId)
            => new Resource { Quantity = 5000, SystemId = systemId, Type = ResourceType.MilkyCoin };

        public static Resource Energy(string systemId)
            => new Resource { Quantity = 5000, SystemId = systemId, Type = ResourceType.Energy };

        public static Resource Fuel(string systemId)
            => new Resource { Quantity = 5000, SystemId = systemId, Type = ResourceType.Fuel };

        public static DefenceBuilder DefenceBuilder(string systemId, string defenceType)
            => new DefenceBuilder { SystemId = systemId, DefensiveStructureType = (DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), defenceType) };

        public static Fleet Fleet(string systemId)
            => new Fleet(systemId);

        public static Ship Ship(string shiptype, string systemId, string playerId)
            => new Ship((ShipType)Enum.Parse(typeof(ShipType), shiptype), systemId, playerId, 0, 0, 0, 250, 0);

    }
}

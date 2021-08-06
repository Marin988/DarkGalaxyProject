using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices
{
    public interface ISystemService
    {
        public SystemServiceModel System(string systemId);

        public IEnumerable<ShipServiceModel> ShipsInSystem(string systemId);

        public IEnumerable<FleetServiceModel> FleetsInSystem(string systemId);

        public HostSystemInfoServiceModel HostSystemInfo(string systemId);

        public IEnumerable<SystemServiceModel> AllSystems(int page);

        public IEnumerable<SystemServiceModel> PlayerSystems(string playerId);

        public IEnumerable<ShipBuilderServiceModel> ShipBuilders(string systemId);

        public IEnumerable<DefenceBuilderServiceModel> DefenceBuilders(string systemId);

        public bool SwitchSystem(string systemId, string playerId);

        public bool Colonize(string systemId, string playerId);

        public string SendFleet(int battleShipCount, int colonizerCount, int transportShipCount, string missionType, int cargo, int destinationSystemPosition, string systemId);

        public bool FleetReturn(string systemId);

        public bool FleetBoarding(string systemId);

        public string StartBuildingShip(string systemId, string shipType, int count);

        public string StartBuildingDefence(string systemId, string defenceType, int count);

        public bool BuildShip(string systemId, string shipType, string playerId);

        public bool BuildDefence(string systemId, string defenceType);
    }
}

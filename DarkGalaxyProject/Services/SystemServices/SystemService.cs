using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices
{
    public class SystemService : ISystemService
    {
        private readonly ApplicationDbContext data;

        public SystemService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<SystemServiceModel> AllSystems(int page)
        {
            var systems = this.data
                .Systems
                .OrderByDescending(s => s.Position)
                .Skip((page - 1) * 5)
                .Take(5)
                .Select(s => new SystemServiceModel
                {
                    Id = s.Id,
                    Position = s.Position,
                    Type = s.Type.ToString(),
                    Ships = s.Ships.Select(sh => new ShipServiceModel
                    {
                        Damage = sh.Damage,
                        HP = sh.HP,
                        Speed = sh.Speed,
                        Storage = sh.Storage,
                        Type = sh.Type.ToString()
                    })
                    .ToList(),
                    Planets = s.Planets.Select(p => new PlanetListingServiceModel
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                    .ToList()
                })
                .ToList();

            return systems;
        }

        public IEnumerable<DefenceBuilderServiceModel> DefenceBuilders(string systemId)
        {
            var defenceBuilders = data.DefenceBuilders
                .Where(sp => sp.SystemId == systemId)
                .Select(sp => new DefenceBuilderServiceModel
                {
                    BuildTime = sp.BuildTime,
                    FinishedBuildingTime = sp.FinishedBuildingTime,
                    systemId = sp.SystemId,
                    DefenceType = sp.DefensiveStructureType.ToString(),
                    Count = sp.Count
                })
                .ToList();

            return defenceBuilders;
        }
       
        public IEnumerable<FleetServiceModel> FleetsInSystem(string systemId)
        {
            var fleets = data.Fleets
                .Where(f => f.SystemId == systemId)
                .Select(f => new FleetServiceModel
                {
                    ArrivalTime = f.ArrivalTime,
                    Outgoing = f.Outgoing,
                    Ships = f.Ships.Select(s => new ShipServiceModel
                    {
                        HP = s.HP,
                        MaxHP = s.MaxHP,
                        Damage = s.Damage,
                        Speed = s.Speed,
                        MaxStorage = s.MaxStorage,
                        Storage = s.Storage,
                        Type = s.Type.ToString(),
                        OnMission = s.OnMission
                    })
                    .ToList()
                })
                .ToList();

            return fleets;
        }

        public HostSystemInfoServiceModel HostSystemInfo(string systemId)
        {
            var HostSystemInfo = data.Systems
                .Where(s => s.Id == systemId)
                .Select(s => new HostSystemInfoServiceModel
                {
                    HostSystemId = s.Id,
                    HostSystemPosition = s.Position,
                    PlayerId = s.PlayerId,
                })
                .First();

            return HostSystemInfo;
        }

        public IEnumerable<SystemServiceModel> PlayerSystems(string playerId)
        {
            var systems = data.Systems
               .Where(s => s.PlayerId == playerId)
               .Select(s => new SystemServiceModel
               {
                   Id = s.Id,
                   Position = s.Position,
                   Type = s.Type.ToString(),
                   Ships = s.Ships.Where(sh => !sh.OnMission).Select(sh => new ShipServiceModel
                   {
                       Damage = sh.Damage,
                       HP = sh.HP,
                       Speed = sh.Speed,
                       Storage = sh.Storage,
                       Type = sh.Type.ToString()
                   })
                   .ToList()
               })
               .ToList();

            return systems;
        }

        public string SendFleet(int battleShipCount, int colonizerCount, int transportShipCount, string missionType, int destinationSystemPosition, string systemId, int cargo)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);
            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId).First(f => f.ArrivalTime == null);
            var missionTypeEnum = (MissionType)Enum.Parse(typeof(MissionType), missionType);

            List<Ship> ships = GetShips(battleShipCount, colonizerCount, transportShipCount, system);//maybe add validations in here

            var destinationSystem = data.Systems.FirstOrDefault(s => s.Position == destinationSystemPosition);

            if (destinationSystem == null || destinationSystemPosition == 0)
            {
                return $"A system with position {destinationSystemPosition} doesn't exist! (or is 0)";
            }

            if (ships.Count == 0)
            {
                return "You cannot send a fleet of 0 ships";
            }

            if (destinationSystemPosition == system.Position)
            {
                return "You cannot send ships to the same system";
            }

            if (missionTypeEnum == MissionType.Colonize)//method  ColonizeErrorChecker
            {
                var error = ColoniseErrorChecker(destinationSystemPosition, system, ships);

                if (error != null)
                {
                    return error;
                }
            }

            if((missionTypeEnum == MissionType.Deploy || missionTypeEnum == MissionType.Transport) && destinationSystem.PlayerId == null)
            {
                return "You cannot deploy ships and transport resources to systems not belonging to a player.";
            }

            if (cargo > 0)
            {
                var systemMilkyCoin = data.Resources.First(r => r.SystemId == system.Id && r.Type == ResourceType.MilkyCoin);

                if(cargo > systemMilkyCoin.Quantity)
                {
                    cargo = systemMilkyCoin.Quantity;
                }

                lock (systemMilkyCoin)
                {
                    systemMilkyCoin.Quantity -= cargo;
                    data.SaveChanges();
                }
            }

            PrepareShipsForMission(cargo, fleet, missionTypeEnum, ships);

            fleet.MissionType = missionTypeEnum;
            fleet.Outgoing = true;
            fleet.Ships.ToList().AddRange(ships);
            fleet.DestinationSystemPoistion = destinationSystemPosition;

            var flightLength = Math.Abs(system.Position - destinationSystemPosition);
            fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);

            var systemFuel = data.Resources.First(r => r.SystemId == system.Id && r.Type == ResourceType.Fuel);
            systemFuel.Quantity -= flightLength * 10;

            data.SaveChanges();

            return null;
        }

        private static List<Ship> GetShips(int battleShipCount, int colonizerCount, int transportShipCount, Data.Models.System system)
        {
            var ships = new List<Ship>();

            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.BattleShip && s.FleetId == null).Take(battleShipCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Colonizer && s.FleetId == null).Take(colonizerCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.TransportShip && s.FleetId == null).Take(transportShipCount));
            return ships;
        }

        private void PrepareShipsForMission(int cargo, Fleet fleet, MissionType missionTypeEnum, List<Ship> ships)
        {
            foreach (var ship in ships)
            {
                ship.OnMission = true;
                ship.FleetId = fleet.Id;

                if (missionTypeEnum == MissionType.Transport || cargo > 0) //possible issue - If there are ships already loaded they might
                {                                                         //be the ones i get in the fleet UNLESS I order them by cargo 
                    int load = ship.MaxStorage - ship.Storage;//or maybe just this
                    if (cargo < load)
                    {
                        load = cargo;
                    }

                    ship.Storage += load;
                    cargo -= load;
                }
            }//issue note (above next to if)
        }

        private string ColoniseErrorChecker(int destinationSystemPosition, Data.Models.System system, List<Ship> ships)
        {
            if (!system.Ships.Any(s => s.Type == ShipType.Colonizer && s.FleetId == null))
            {
                return $"You don't have any {ShipType.Colonizer.ToString()}s";
            }
            if (data.Systems.First(s => s.Position == destinationSystemPosition).PlayerId != null)
            {
                return $"You cannot colonise a system, belonging to a player!";
            }
            if (!ships.Any(s => s.Type == ShipType.Colonizer))
            {
                ships.Add(system.Ships.First(s => s.Type == ShipType.Colonizer && s.FleetId == null));//or return error
            }

            return null;
        }

        public IEnumerable<ShipBuilderServiceModel> ShipBuilders(string systemId)
        {
            var shipBuilders = data.ShipBuilders
                .Where(sp => sp.SystemId == systemId)
                .Select(sp => new ShipBuilderServiceModel
                {
                    BuildTime = sp.BuildTime,
                    FinishedBuildingTime = sp.FinishedBuildingTime,
                    systemId = sp.SystemId,
                    ShipType = sp.ShipType.ToString(),
                    Count = sp.Count
                })
                .ToList();

            return shipBuilders;
        }

        public IEnumerable<ShipServiceModel> ShipsInSystem(string systemId)
        {
            var ships = data.Ships
                .Where(s => s.SystemId == systemId)
                .Select(s => new ShipServiceModel
                {
                    HP = s.HP,
                    MaxHP = s.MaxHP,
                    Damage = s.Damage,
                    Speed = s.Speed,
                    MaxStorage = s.MaxStorage,
                    Storage = s.Storage,
                    Type = s.Type.ToString(),
                    OnMission = s.OnMission,
                    DealId = s.DealId,
                    FleetId = s.FleetId
                })
                .ToList();

            return ships;
        }

        public string StartBuildingDefence(string systemId, string defenceType, int count)
        {
            var system = data.Systems.Include(s => s.DefenceBuildingQueue).First(s => s.Id == systemId);

            var defencetype = (DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), defenceType);

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if(systemMilkyCoin.Quantity < (int)defencetype * 100 * count)
            {
                return $"You don't have enough {systemMilkyCoin.Type.ToString()}";
            }

            systemMilkyCoin.Quantity -= (int)defencetype * 100 * count;

            var defenceBuildingQueue = system.DefenceBuildingQueue.First(s => s.DefensiveStructureType == defencetype);

            defenceBuildingQueue.Count = count;

            defenceBuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(defenceBuildingQueue.BuildTime);

            data.SaveChanges();

            return null;
        }

        public string StartBuildingShip(string systemId, string shipType, int count)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var playerId = system.PlayerId;

            ResearchType researchType = 0;
            switch (shiptype)
            {
                case ShipType.Colonizer:
                    researchType = ResearchType.Colonizing;
                    break;
                case ShipType.TransportShip:
                    researchType = ResearchType.TransportShip;
                    break;
                case ShipType.BattleShip:
                    researchType = ResearchType.BattleShip;
                    break;
                default:
                    break;
            };

            var research = data.ResearchTrees.FirstOrDefault(r => r.PlayerId == playerId && r.ResearchType == researchType);

            if (!research.IsLearned)
            {
                return $"You have to research {researchType.ToString()} before building ship of type {shiptype.ToString()}";
            }

            var shipbuildingQueue = system.ShipBuildingQueue.FirstOrDefault(s => s.ShipType == shiptype);

            shipbuildingQueue.Count = count;
            shipbuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(shipbuildingQueue.BuildTime);

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if (systemMilkyCoin.Quantity < (int)shiptype * 500 * count)
            {
                return $"You don't have enough {systemMilkyCoin.Type.ToString()}";
            }

            systemMilkyCoin.Quantity -= (int)shiptype * 500 * count;

            data.SaveChanges();

            return null;
        }

        public bool SwitchSystem(string systemId, string playerId)
        {
            var player = data.Players.First(p => p.Id == playerId);

            player.CurrentSystemId = systemId;

            data.SaveChanges();

            return true;
        }

        public SystemServiceModel System(string systemId)
        {
            var system = data.Systems.Where(s => s.Id == systemId).Select(s => new SystemServiceModel
            {
                Id = s.Id,
                PlayerId = s.PlayerId,
                UserName = s.Player.UserName,
                Position = s.Position,
                Type = s.Type.ToString(),
                Ships = s.Ships.Select(sh => new ShipServiceModel
                {
                    Damage = sh.Damage,
                    HP = sh.HP,
                    Speed = sh.Speed,
                    Storage = sh.Storage,
                    Type = sh.Type.ToString()
                }),
                Planets = s.Planets.Select(p => new PlanetListingServiceModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
               .ToList()
            })
           .FirstOrDefault();

            return system;
        }

        public string AddFleet(string systemId, string playerId)
        {
            var fleetCount = data.Fleets.Count(f => f.SystemId == systemId);

            if(fleetCount == 5)
            {
                return "You already have the maximum of 5 fleets";
            }

            ResearchType researchType = ResearchType.SecondFleet;

            switch (fleetCount)
            {
                case 1:
                    researchType = ResearchType.SecondFleet;
                    break;
                case 2:
                    researchType = ResearchType.ThirdFleet;
                    break;
                case 3:
                    researchType = ResearchType.FourthFleet;
                    break;
                case 4:
                    researchType = ResearchType.FifthFleet;
                    break;
            }

            var FleetResearch = data.ResearchTrees.First(r => r.PlayerId == playerId && r.ResearchType == researchType);

            if (!FleetResearch.IsLearned)
            {
                return $"You have not yet learned the {researchType.ToString()} research";
            }

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if(systemMilkyCoin.Quantity < 10000 * fleetCount)
            {
                return $"You don't have enough {systemMilkyCoin.Type.ToString()}";
            }

            systemMilkyCoin.Quantity -= 10000 * fleetCount;

            var fleet = new Fleet(systemId);

            data.Fleets.Add(fleet);

            data.SaveChanges();

            return "You have successfully added a new fleet";
        }
    }
}

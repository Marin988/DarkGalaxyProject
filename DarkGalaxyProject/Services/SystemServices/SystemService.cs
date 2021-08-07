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

        public bool BuildDefence(string systemId, string defenceType)
        {
            var system = data.Systems.Include(s => s.DefenceBuildingQueue).First(s => s.Id == systemId);

            var defencetype = (DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), defenceType);

            var defenceBuildingQueue = system.DefenceBuildingQueue.First(s => s.DefensiveStructureType == defencetype);

            defenceBuildingQueue.FinishedBuildingTime = null;

            for (int i = 0; i < defenceBuildingQueue.Count; i++)
            {
                data.DefensiveStructures.Add(new DefensiveStructure(defencetype, systemId));
            }

            defenceBuildingQueue.Count = 0;

            data.SaveChanges();

            return true;
        }

        public bool BuildShip(string systemId, string shipType, string playerId)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var shipBuildingQueue = system.ShipBuildingQueue.First(s => s.ShipType == shiptype);

            shipBuildingQueue.FinishedBuildingTime = null;

            for (int i = 0; i < shipBuildingQueue.Count; i++)
            {
                data.Ships.Add(new Ship(shiptype, systemId, playerId));
            }

            shipBuildingQueue.Count = 0;

            data.SaveChanges();

            return true;
        }

        public bool Colonize(string systemId, string playerId)
        {
            var targetedSystem = data.Systems.First(s => s.Id == systemId);

            if (targetedSystem.PlayerId != null)
            {
                return false;
            }

            targetedSystem.PlayerId = playerId;

            data.SaveChanges();

            return true;
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

        public bool FleetBoarding(string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId && f.ArrivalTime.HasValue).FirstOrDefault(f => f.ArrivalTime.Value <= DateTime.Now);//here

            if (fleet != null)
            {
                var ships = fleet.Ships.ToList();

                foreach (var ship in ships)
                {
                    ship.OnMission = false;
                    ship.FleetId = null;
                }

                fleet.ArrivalTime = null;

                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool FleetReturn(string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId && f.ArrivalTime.HasValue).FirstOrDefault(f => f.ArrivalTime.Value <= DateTime.Now);

            string playerId = system.PlayerId;

            if (fleet == null)
            {
                //add errors
                return false;
            }

            fleet.Outgoing = false;

            var destinationSystem = data.Systems.Include(s => s.Ships).Include(s => s.DefensiveStructures).First(s => s.Position == fleet.DestinationSystemPoistion);

            var ShipsOnMission = fleet.Ships.ToList();

            var flightLength = Math.Abs(system.Position - (int)fleet.DestinationSystemPoistion);
            fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);
            fleet.DestinationSystemPoistion = null;

            if (fleet.MissionType == MissionType.Attack) // separate method
            {
                Battle(fleet, destinationSystem, ShipsOnMission, playerId, destinationSystem.PlayerId);
            } // separate method

            if (fleet.MissionType == MissionType.Transport)
            {
                CargoUnload(playerId, destinationSystem, ShipsOnMission);
            }


            if (fleet.MissionType == MissionType.Deploy)//I may add transport function in here later // separate method
            {
                DeployShips(playerId, fleet, destinationSystem, ShipsOnMission);
            }//NOTE


            if (fleet.MissionType == MissionType.Colonize)//I may add transport function in here later // separate method
            {
                Colonize(playerId, fleet, destinationSystem, ShipsOnMission);
            }//NOTE

            data.SaveChanges();

            return true;
        }

        private void Colonize(string playerId, Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission)
        {
            var colonizer = fleet.Ships.First(s => s.Type == ShipType.Colonizer);
            if (ShipsOnMission.Count == 1)
            {
                fleet.ArrivalTime = null;
            }
            destinationSystem.PlayerId = playerId;//now this would playerId have to be changed if I put that in the background service
            data.Ships.Remove(colonizer);
            var messageTitle = $"Colonised system";
            var messageContent = $"Successfully colonised system {destinationSystem.Position}";

            ReportMessage(playerId, messageTitle, messageContent);
        }

        private void DeployShips(string senderplayerId, Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission)
        {
            fleet.ArrivalTime = null;
            var senderUsername = data.Players.First(p => p.Id == senderplayerId).UserName;
            var systemPosition = data.Systems.First(s => s.Id == ShipsOnMission.First().SystemId).Position;
            foreach (var ship in ShipsOnMission)
            {
                ship.SystemId = destinationSystem.Id;
                ship.FleetId = null;
                ship.OnMission = false;
                ship.PlayerId = destinationSystem.PlayerId;//check if dest system has a playerId if you let that be acceptable in your business logic
            }
            var recieverPlayerId = destinationSystem.PlayerId;

            var MessageTitle = $"Deployed ships";
            var recieverMessageContent = $"{ShipsOnMission.Count} ships were deployed to your system {destinationSystem.Position} by {senderUsername} from {systemPosition}";
            var senderMessageContent = $"You deployed {ShipsOnMission.Count} ships to system {destinationSystem.Position}";

            ShipsOnMission.RemoveAll(p => !p.OnMission);

            ReportMessage(senderplayerId, MessageTitle, senderMessageContent);
            ReportMessage(recieverPlayerId, MessageTitle, recieverMessageContent);

        }

        private void CargoUnload(string PlayerId, Data.Models.System destinationSystem, List<Ship> ShipsOnMission)
        {
            var transportedResources = ShipsOnMission.Sum(s => s.Storage);
            var playerName = data.Players.First(p => p.Id == PlayerId).UserName;
            var systemId = ShipsOnMission.First().SystemId;
            var systemPosition = data.Systems.First(s => s.Id == systemId).Position;

            foreach (var ship in ShipsOnMission)
            {
                ship.Storage = 0;
            }

            destinationSystem.Resources.First(r => r.Type == ResourceType.MilkyCoin).Quantity += transportedResources;

            var SenderMessageTitle = "Transported resources";
            var SenderMessageContent = $"You've successfully transported {transportedResources} {ResourceType.MilkyCoin.ToString()} to system {destinationSystem.Position}.";

            var RecieverMessageTitle = "Recieved resources";
            var RecieverMessageContent = $"You have recieved {transportedResources} {ResourceType.MilkyCoin.ToString()} by {playerName} from system {systemPosition}.";

            ReportMessage(PlayerId, SenderMessageTitle, SenderMessageContent);
            ReportMessage(destinationSystem.PlayerId, RecieverMessageTitle, RecieverMessageContent);

        }

        private void Battle(Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, string playerId, string destinationSystemPlayerId)
        {
            var attackerHP = ShipsOnMission.Sum(s => s.HP);
            var attackerDMG = ShipsOnMission.Sum(s => s.Damage);

            var DefenderFleet = destinationSystem.Ships.Where(s => !s.OnMission);
            var Defence = destinationSystem.DefensiveStructures;

            var defenderHP = DefenderFleet.Sum(f => f.HP) + Defence.Sum(d => d.HP);
            var defenderDMG = DefenderFleet.Sum(f => f.Damage) + Defence.Sum(d => d.Damage);

            var messageTitle = "Battle report";
            var AttackerMessageContent = string.Empty;
            var DefenderMessageContent = string.Empty;

            if (attackerDMG >= defenderHP)
            {
                data.Ships.RemoveRange(DefenderFleet);
                data.DefensiveStructures.RemoveRange(Defence);

                AttackerMessageContent += $"Attack successful.{DefenderFleet.Count()} ships and {Defence.Count()} defensive structures were destroyed.";
                DefenderMessageContent += $"You were attacked by {ShipsOnMission.Count} ships.All of your {DefenderFleet.Count()} ships and {Defence.Count()} defensive structures were destroyed.";

                DefenderFleet.ToList().RemoveAll(s => !s.OnMission);
                Defence.ToList().RemoveAll(d => d.SystemId == destinationSystem.Id); //am I actually removing them?!? 
            }
            else
            {
                foreach (var def in Defence)
                {
                    var defHP = def.HP;
                    if (def.HP <= attackerDMG)
                    {
                        def.HP -= attackerDMG;
                    }
                    else
                    {
                        def.HP -= attackerDMG;
                        attackerDMG = 0;
                        break;
                    }
                    attackerDMG -= defHP;
                }

                if (attackerDMG > 0)
                {
                    foreach (var defShip in DefenderFleet)
                    {
                        if (defShip.HP <= attackerDMG)
                        {
                            defShip.HP -= attackerDMG;
                        }
                        else
                        {
                            defShip.HP -= attackerDMG;
                            attackerDMG = 0;
                            break;
                        }
                        attackerDMG -= defShip.HP;
                    }
                }

                if (Defence.Any(d => d.HP <= 0))
                {
                    Defence.ToList().RemoveAll(d => d.HP <= 0);
                }
                if (DefenderFleet.Any(s => s.HP <= 0))
                {
                    DefenderFleet.ToList().RemoveAll(s => s.HP <= 0);
                }

            }
            if (defenderDMG >= attackerHP)
            {
                data.Ships.RemoveRange(ShipsOnMission);

                AttackerMessageContent += $"Your {ShipsOnMission.Count} ships were destroyed by the enemy defences, surviving your attack with {Defence.Sum(d => d.HP)} HP left.";
                DefenderMessageContent += $"Your defences destroyed all {ShipsOnMission.Count} of the enemy's ships, surviving the attack with {Defence.Sum(d => d.HP)} HP left.";

                ShipsOnMission.RemoveAll(s => s.OnMission);
                fleet.ArrivalTime = null;
                ReportMessage(playerId, messageTitle, AttackerMessageContent);
                if(destinationSystemPlayerId != null)
                {
                    ReportMessage(destinationSystemPlayerId, messageTitle, DefenderMessageContent);
                }
            }
            else
            {
                foreach (var ship in ShipsOnMission)
                {
                    var shipHP = ship.HP;
                    if (ship.HP <= defenderDMG)
                    {
                        ship.HP -= defenderDMG;
                    }
                    else
                    {
                        ship.HP -= defenderDMG;
                        defenderDMG = 0;
                        break;
                    }
                    defenderDMG -= shipHP;
                }

                if (ShipsOnMission.Any(s => s.HP <= 0))
                {
                    ShipsOnMission.RemoveAll(s => s.HP <= 0);
                }
            }

            if (ShipsOnMission.Any()) //I presume that if there are any ships left all the defences have been wiped out
            {
                var systemResource = data.Resources.First(r => r.SystemId == destinationSystem.Id && r.Type == ResourceType.MilkyCoin);
                var loot = 0;

                foreach (var ship in ShipsOnMission)
                {
                    var availableStorage = ship.MaxStorage - ship.Storage; //may include that as a property

                    if (ship.Storage < ship.MaxStorage)
                    {
                        if (systemResource.Quantity >= availableStorage)
                        {
                            ship.Storage = ship.MaxStorage;
                            systemResource.Quantity -= availableStorage;
                            loot += availableStorage;
                        }
                        else
                        {
                            ship.Storage += systemResource.Quantity;
                            loot += systemResource.Quantity;
                            systemResource.Quantity = 0;
                            break;
                        }
                    }
                }

                AttackerMessageContent += $"The enemy defences were destroyed, your fleet looted {loot} {systemResource.Type.ToString()}s and is coming back.";
                DefenderMessageContent += $"The enemy ships looted {loot} {systemResource.Type.ToString()}s.";

                ReportMessage(playerId, messageTitle, AttackerMessageContent);
                ReportMessage(destinationSystemPlayerId, messageTitle, DefenderMessageContent);
            }
        }

        private void ReportMessage(string playerId, string messageTitle, string messageContent)
        {
            var reportMessage = new Message()
            {
                ReceiverId = playerId,
                TimeOfSending = DateTime.Now,
                SenderId = "1",//baaaaaaaaaaaad------Add a "System" user? Who would send all these messages
                Title = messageTitle,
                Content = messageContent
            };

            data.Messages.Add(reportMessage);
            data.SaveChanges();
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

        public string SendFleet(int battleShipCount, int colonizerCount, int transportShipCount, string missionType, int cargo, int destinationSystemPosition, string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);
            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId).First(f => f.ArrivalTime == null);
            var missionTypeEnum = (MissionType)Enum.Parse(typeof(MissionType), missionType);

            List<Ship> ships = GetShips(battleShipCount, colonizerCount, transportShipCount, system);//maybe add validations in here

            var destinationSystem = data.Systems.FirstOrDefault(s => s.Position == destinationSystemPosition);

            if (destinationSystem == null)
            {
                return $"A system with position {destinationSystemPosition} doesn't exist!";
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

            if (missionTypeEnum == MissionType.Transport && cargo > 0)
            {
                var systemMilkyCoin = system.Resources.First(r => r.Type == ResourceType.MilkyCoin).Quantity;

                if(cargo > systemMilkyCoin)
                {
                    cargo = systemMilkyCoin;
                }

                systemMilkyCoin -= cargo;
            }

            PrepareShipsForMission(cargo, fleet, missionTypeEnum, ships);

            fleet.MissionType = missionTypeEnum;
            fleet.Outgoing = true;
            fleet.Ships.ToList().AddRange(ships);
            fleet.DestinationSystemPoistion = destinationSystemPosition;

            var flightLength = Math.Abs(system.Position - destinationSystemPosition);
            fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);

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

                if (missionTypeEnum == MissionType.Transport && cargo > 0) //possible issue - If there are ships already loaded they might
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
    }
}

using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.SystemServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DarkGalaxyProject.Services.SystemServices
{
    using static GlobalConstants.SystemConstants;
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

        public IEnumerable<FleetServiceModel> AllFleets(string playerId)
        {
            var playersFleets = data.Systems
                .Include(s => s.Fleets.Where(f => f.ArrivalTime != null))
                .ThenInclude(f => f.Ships)
                .Where(s => s.PlayerId == playerId || s.CurrentPlayerId == playerId)
                .ToList()
                .SelectMany(s => s.Fleets.Select(f => new FleetServiceModel
                {
                    Ships = f.Ships.Select(sh => new ShipServiceModel
                    {
                        Damage = sh.Damage,
                        HP = sh.HP,
                        Speed = sh.Speed,
                        Storage = sh.Storage,
                        Type = sh.Type.ToString()
                    })
                    .ToList(),
                    ArrivalTime = f.ArrivalTime,
                    Outgoing = f.Outgoing,
                    DestinationSystemPosition = f.DestinationSystemPoistion,
                })
                .ToList())
                .ToList();

            return playersFleets;
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
                    Count = sp.Count,
                    PricePerUnit = sp.PricePerUnit,
                    TotalPrice = sp.TotalPrice
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
                        OnMission = s.OnMission,
                        FuelExpense = s.FuelExpense
                    })
                    .ToList(),
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
                   .ToList(),
                   Defences = s.DefensiveStructures.Select(d => new DefenceServiceModel
                   {
                       Type = d.Type.ToString(),
                       Damage = d.Damage,
                       HP = d.HP
                   })
                   .ToList()
               })
               .ToList();

            return systems;
        }

        public string SendFleet(int battleShipCount, int colonizerCount, int transportShipCount, string missionType, int destinationSystemPosition, string systemId, int cargo)
        {
            var AvailableFleet = data.Fleets.FirstOrDefault(f => f.SystemId == systemId && f.ArrivalTime == null);

            if (AvailableFleet == null)
            {
                return NoAvailableFleets;
            }

            var missionTypeEnum = (MissionType)Enum.Parse(typeof(MissionType), missionType);

            if (battleShipCount < 1 && transportShipCount < 1 && missionTypeEnum != MissionType.Spy && missionTypeEnum != MissionType.Colonize)
            {
                return FleetShouldHaveAtLeastOneShip;
            }

            var destinationSystem = data.Systems.FirstOrDefault(s => s.Position == destinationSystemPosition);
            var hostSystem = data.Systems.First(s => s.Id == systemId);

            if ((missionTypeEnum == MissionType.Attack || missionTypeEnum == MissionType.Spy || missionTypeEnum == MissionType.Colonize) && destinationSystem.PlayerId == hostSystem.PlayerId)
            {
                return NoHostileMissionsOnOwnSystems;
            }

            if (destinationSystem == null || destinationSystemPosition < 1 || destinationSystemPosition > 100)
            {
                return string.Format(CannotFindSystemWithThisPosition, destinationSystemPosition);
            }

            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            List<Ship> ships = GetShips(battleShipCount, colonizerCount, transportShipCount, system);

            if (ships.Count == 0 && missionTypeEnum != MissionType.Spy && missionTypeEnum != MissionType.Colonize)
            {
                return string.Format(NoShipsOnSystemSuitableForThisMission, missionType);
            }

            if (destinationSystemPosition == system.Position)
            {
                return CannotSendShipsToTheSameSystem;
            }

            if (missionTypeEnum == MissionType.Colonize)
            {
                var error = ColoniseErrorChecker(destinationSystemPosition, system, ships);

                if (error != null)
                {
                    return error;
                }
            }

            if ((missionTypeEnum == MissionType.Deploy || missionTypeEnum == MissionType.Transport) && destinationSystem.PlayerId == null)
            {
                return DeployAndTransportTargetSystemsShouldBelongToPlayer;
            }

            if(missionTypeEnum == MissionType.Colonize && destinationSystem.PlayerId != null)
            {
                return CannotColoniseSystemsBelongingToOtherPlayers;
            }

            if ((missionTypeEnum == MissionType.Spy || missionTypeEnum == MissionType.Attack) && cargo > 0)
            {
                return SpyAndAttackMissionsCannotTransport;
            }

            if (cargo > ships.Sum(s => s.MaxStorage))
            {
                cargo = ships.Sum(s => s.MaxStorage);
            }

            if (cargo > 0)
            {
                var systemMilkyCoin = data.Resources.First(r => r.SystemId == system.Id && r.Type == ResourceType.MilkyCoin);

                if (cargo > systemMilkyCoin.Quantity)
                {
                    cargo = systemMilkyCoin.Quantity;
                }

                systemMilkyCoin.Quantity -= cargo;
                //data.SaveChanges();
            }

            if (missionTypeEnum == MissionType.Spy)
            {
                if (!system.Ships.Any(s => s.Type == ShipType.Espionage))
                {
                    return NeedsEspionageToSpy;
                }
                if (ships.Any())
                {
                    return OnlyEspionagesCanBeSentOnSpyMission;
                }

                ships.Add(system.Ships.First(s => s.Type == ShipType.Espionage));
            }

            var systemFuel = data.Resources.First(r => r.SystemId == system.Id && r.Type == ResourceType.Fuel);
            var flightLength = Math.Abs(system.Position - destinationSystemPosition);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId).First(f => f.ArrivalTime == null);

            var fuelConsumption = ships.Sum(s => s.FuelExpense) * flightLength;
            var fleetSpeed = ships.Min(s => s.Speed);

            if (systemFuel.Quantity < fuelConsumption)
            {
                return string.Format(InsufficientFuel, fuelConsumption, systemFuel.Quantity);
            }

            PrepareShipsForMission(cargo, fleet, missionTypeEnum, ships);

            fleet.MissionType = missionTypeEnum;
            fleet.Outgoing = true;
            fleet.Ships.ToList().AddRange(ships);
            fleet.DestinationSystemPoistion = destinationSystemPosition;
            fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength * 2000 / fleetSpeed);

            systemFuel.Quantity -= fuelConsumption;

            var saved = false;

            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    data.SaveChanges();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Resource)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }

            return string.Format(FleetSentOnMission, fleet.Ships.Count(), missionTypeEnum.ToString(), destinationSystemPosition);
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

                if (cargo > 0)
                {
                    int load = ship.MaxStorage - ship.Storage;
                    if (cargo < load)
                    {
                        load = cargo;
                    }

                    ship.Storage += load;
                    cargo -= load;
                }
            }
        }

        private string ColoniseErrorChecker(int destinationSystemPosition, Data.Models.System system, List<Ship> ships)
        {
            if (!system.Ships.Any(s => s.Type == ShipType.Colonizer && s.FleetId == null))
            {
                return string.Format(NotEnoughShipsOfType, ShipType.Colonizer.ToString());
            }
            if (data.Systems.First(s => s.Position == destinationSystemPosition).PlayerId != null)
            {
                return CannotColoniseSystemsBelongingToOtherPlayers;
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
                    Count = sp.Count,
                    TotalPrice = sp.TotalPrice,
                    PricePerShip = sp.PricePerShip
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
                    FleetId = s.FleetId,
                    FuelExpense = s.FuelExpense
                })
                .ToList();

            return ships;
        }

        public string StartBuildingDefence(string systemId, string defenceTypeString, int count, string playerId)
        {
            if (count < 1)
            {
                return ValidCount;
            }

            var system = data.Systems.Include(s => s.DefenceBuildingQueue).First(s => s.Id == systemId);

            if (playerId != system.PlayerId)
            {
                return CanOnlyBuildOnOwnSystems;
            }

            if (system.DefenceBuildingQueue.Any(s => s.FinishedBuildingTime != null))
            {
                return DefenceIsAlreadyBuilding;
            }

            var research = data.ResearchTrees.First(r => r.PlayerId == playerId && r.ResearchType == ResearchType.HeavyDefence);
            var defenceType = (DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), defenceTypeString);

            if (defenceType == DefensiveStructureType.SpaceStation && !research.IsLearned)
            {
                return string.Format(MustLearnResearchBeforeBuilding, research.ResearchType.ToString(), DefensiveStructureType.SpaceStation.ToString());
            }

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);
            var defenceBuildingQueue = system.DefenceBuildingQueue.First(s => s.DefensiveStructureType == defenceType);
            defenceBuildingQueue.Count = count;

            if (systemMilkyCoin.Quantity < defenceBuildingQueue.TotalPrice)
            {
                return string.Format(InsufficientResourcesForBuilding, defenceBuildingQueue.TotalPrice, systemMilkyCoin.Type.ToString(), count, defenceBuildingQueue.DefensiveStructureType.ToString(), systemMilkyCoin.Quantity);
            }

            systemMilkyCoin.Quantity -= defenceBuildingQueue.TotalPrice;

            defenceBuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(defenceBuildingQueue.BuildTime);


            data.SaveChanges();

            return string.Format(StartedBuilding, count, defenceBuildingQueue.DefensiveStructureType.ToString(), defenceBuildingQueue.TotalPrice, systemMilkyCoin.Type.ToString());
        }

        public string StartBuildingShip(string systemId, string shipType, int count, string playerId)
        {
            if (count < 1)
            {
                return ValidCount;
            }

            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            if (playerId != system.PlayerId)
            {
                return CanOnlyBuildOnOwnSystems;
            }

            if(system.ShipBuildingQueue.Any(s => s.FinishedBuildingTime != null))
            {
                return ShipIsAlreadyBuilding;
            }

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);
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
                case ShipType.Espionage:
                    researchType = ResearchType.Espionage;
                    break;
                default:
                    break;
            };

            var research = data.ResearchTrees.FirstOrDefault(r => r.PlayerId == playerId && r.ResearchType == researchType);

            if (!research.IsLearned)
            {
                return string.Format(NeedResearchForBuildingShip, researchType.ToString(), shiptype.ToString());
            }

            var shipbuildingQueue = system.ShipBuildingQueue.FirstOrDefault(s => s.ShipType == shiptype);
            shipbuildingQueue.Count = count;

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if (systemMilkyCoin.Quantity < shipbuildingQueue.TotalPrice)
            {
                return string.Format(InsufficientResourcesForBuilding, shipbuildingQueue.TotalPrice, systemMilkyCoin.Type.ToString(), count, shipbuildingQueue.ToString(), systemMilkyCoin.Quantity);
            }

            systemMilkyCoin.Quantity -= shipbuildingQueue.TotalPrice;

            shipbuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(shipbuildingQueue.BuildTime);


            data.SaveChanges();

            return string.Format(StartedBuilding, count, shipbuildingQueue.ShipType.ToString(), shipbuildingQueue.TotalPrice, systemMilkyCoin.Type.ToString());
        }

        public bool SwitchSystem(string systemId, string playerId)
        {
            var player = data.Players.First(p => p.Id == playerId);
            var system = data.Systems.First(s => s.Id == systemId);
            var currentSystemsCheck = data.Systems
                .Where(s => s.CurrentPlayerId == playerId)
                .ToList();
            foreach (var curentSystem in currentSystemsCheck)
            {
                curentSystem.CurrentPlayerId = null;
            }


            player.CurrentSystemId = systemId;
            system.CurrentPlayerId = playerId;

            data.SaveChanges();

            return true;
        }

        public SystemServiceModel System(string systemId)
        {
            var system = data.Systems
                .Include(s => s.DefensiveStructures)
                .Where(s => s.Id == systemId)
                .Select(s => new SystemServiceModel
                {
                    Id = s.Id,
                    PlayerId = s.PlayerId,
                    UserName = s.Player.UserName,
                    Position = s.Position,
                    Type = s.Type.ToString(),
                    Planets = s.Planets.Select(p => new PlanetListingServiceModel
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                    .OrderBy(p => p.Name)
                    .ToList(),
                    Defences = s.DefensiveStructures.Select(d => new DefenceServiceModel
                    {
                        Type = d.Type.ToString(),
                        Damage = d.Damage,
                        HP = d.HP
                    })
                    .ToList()
                })
           .FirstOrDefault();

            return system;
        }

        public string AddFleet(string systemId, string playerId)
        {
            var fleetCount = data.Fleets.Count(f => f.SystemId == systemId);

            if (fleetCount == 5)
            {
                return AlreadyHaveFiveFleets;
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
                return string.Format(ResearchNotLearned, researchType.ToString());
            }

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            if (systemMilkyCoin.Quantity < 10000 * fleetCount)
            {
                return string.Format(InsufficientResources, 10000 * fleetCount, systemMilkyCoin.Type.ToString(), systemMilkyCoin.Quantity);
            }

            systemMilkyCoin.Quantity -= 10000 * fleetCount;

            var fleet = new Fleet(systemId);

            data.Fleets.Add(fleet);

            data.SaveChanges();

            return NewFleetAdded;
        }

        public string CurrentSystemId(string playerId)
        {
            var currentSystemId = data.Players
                .First(p => p.Id == playerId)
                .CurrentSystemId;

            return currentSystemId;
        }
    }
}

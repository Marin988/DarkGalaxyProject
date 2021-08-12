using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.SystemServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DarkGalaxyProject.BackgroundTasks
{
    public class ResourceGrowing : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public ResourceGrowing(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var resources = dbContext.Resources;

                    foreach (var resource in resources)
                    {
                        var planetsId = dbContext.Planets.Where(p => p.SystemId == resource.SystemId).Select(p => p.Id).ToList();
                        var sun = dbContext.Suns.First(s => s.SystemId == resource.SystemId);

                        var totalIncome = 0;

                        FactoryType factoryType = 0;

                        switch (resource.Type)
                        {
                            case ResourceType.MilkyCoin:
                                factoryType = FactoryType.Factory;
                                break;
                            case ResourceType.Fuel:
                                factoryType = FactoryType.Factory;
                                break;
                            case ResourceType.Paper:
                                factoryType = FactoryType.ResearchBuilding;
                                break;
                            case ResourceType.Energy:
                                factoryType = FactoryType.SolarPanel;
                                break;
                        }

                        foreach (var planetId in planetsId)
                        {
                            totalIncome += dbContext.Factories.First(f => f.PlanetId == planetId && f.FactoryType == factoryType).Income;
                        }

                        if(resource.Type == ResourceType.Energy)
                        {
                            totalIncome *= (int)sun.Type;
                        }

                        if(resource.Type == ResourceType.Fuel)
                        {
                            totalIncome /= 2;
                        }

                        resource.Quantity += totalIncome;
                    }

                    dbContext.SaveChanges();

                    if(dbContext.Factories.Any(f => f.UpgradeFinishTime <= DateTime.Now))
                    {
                        foreach (var factory in dbContext.Factories.Where(f => f.UpgradeFinishTime <= DateTime.Now))
                        {
                            factory.Level += 1;
                            factory.UpgradeFinishTime = null;
                        }
                    }

                    if (dbContext.Fleets.Any(f => f.ArrivalTime <= DateTime.Now))
                    {
                        foreach (var fleet in dbContext.Fleets.Where(f => f.ArrivalTime <= DateTime.Now))
                        {
                            if (fleet.Outgoing)
                            {
                                FleetReturn(fleet.SystemId, dbContext);
                            }
                            else
                            {
                                FleetBoarding(fleet.SystemId, dbContext);
                            }
                        }
                    }

                    if (dbContext.ShipBuilders.Any(s => s.FinishedBuildingTime <= DateTime.Now))
                    {
                        List<Ship> ships = new List<Ship>();
                        foreach (var shipBuilder in dbContext.ShipBuilders.Where(s => s.FinishedBuildingTime <= DateTime.Now))
                        {
                            var playerId = dbContext.Systems.First(s => s.Id == shipBuilder.SystemId).PlayerId;

                            for (int i = 0; i < shipBuilder.Count; i++)
                            {
                                ships.Add(new Ship(shipBuilder.ShipType, shipBuilder.SystemId, playerId));
                            }

                            shipBuilder.FinishedBuildingTime = null;
                            shipBuilder.Count = 0;
                        }

                        dbContext.AddRange(ships);
                    }

                    if(dbContext.DefenceBuilders.Any(d => d.FinishedBuildingTime <= DateTime.Now))
                    {
                        List <DefensiveStructure> defences = new List<DefensiveStructure>();
                        foreach (var defenceBuilder in dbContext.DefenceBuilders.Where(d => d.FinishedBuildingTime <= DateTime.Now))
                        {
                            for (int i = 0; i < defenceBuilder.Count; i++)
                            {
                                defences.Add(new DefensiveStructure(defenceBuilder.DefensiveStructureType, defenceBuilder.SystemId));
                            }

                            defenceBuilder.FinishedBuildingTime = null;
                            defenceBuilder.Count = 0;
                        }

                        dbContext.AddRange(defences);
                    }


                    await dbContext.SaveChangesAsync();//cancel token None - read about it

                    await Task.Delay(1000, stoppingToken);
                }
            }
        }

        public bool FleetReturn(string systemId, ApplicationDbContext data)
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

            var ShipsOnMission = fleet.Ships.OrderByDescending(s => s.Type).ToList();

            var flightLength = Math.Abs(system.Position - (int)fleet.DestinationSystemPoistion);
            fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);
            fleet.DestinationSystemPoistion = null;

            if (fleet.MissionType == MissionType.Attack) // separate method
            {
                Battle(fleet, destinationSystem, ShipsOnMission, playerId, destinationSystem.PlayerId, data);
            } // separate method

            if ((fleet.MissionType == MissionType.Transport || fleet.MissionType == MissionType.Deploy) && ShipsOnMission.Sum(s => s.Storage) > 0)
            {
                CargoUnload(playerId, destinationSystem, ShipsOnMission, data);
            }


            if (fleet.MissionType == MissionType.Deploy)//I may add transport function in here later // separate method
            {
                DeployShips(playerId, fleet, destinationSystem, ShipsOnMission, data);
            }//NOTE


            if (fleet.MissionType == MissionType.Colonize)//I may add transport function in here later // separate method
            {
                fleet = Battle(fleet, destinationSystem, ShipsOnMission, playerId, destinationSystem.PlayerId, data);
                ShipsOnMission = fleet.Ships.OrderByDescending(s => s.Type).ToList();
                Colonize(playerId, fleet, destinationSystem, ShipsOnMission, data);
            }//NOTE

            //data.SaveChanges();

            return true;
        }

        private void Colonize(string playerId, Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, ApplicationDbContext data)
        {
            var colonizer = fleet.Ships.FirstOrDefault(s => s.Type == ShipType.Colonizer);
            string messageTitle;
            string messageContent;

            if (colonizer == null || ShipsOnMission.Count == 0)
            {
                fleet.ArrivalTime = null;
                messageTitle = $"Colonization failed";
                messageContent = $"Your coloniser was destroyed at system {destinationSystem.Position}";

                ReportMessage(playerId, messageTitle, messageContent, data);
                return;
            }

            if (ShipsOnMission.Count == 1)
            {
                fleet.ArrivalTime = null;
            }
            destinationSystem.PlayerId = playerId;
            data.Ships.Remove(colonizer);
            messageTitle = $"Colonised system";
            messageContent = $"Successfully colonised system {destinationSystem.Position}";

            ReportMessage(playerId, messageTitle, messageContent, data);
        }

        private void DeployShips(string senderplayerId, Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, ApplicationDbContext data)
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

            ReportMessage(senderplayerId, MessageTitle, senderMessageContent, data);
            ReportMessage(recieverPlayerId, MessageTitle, recieverMessageContent, data);

        }

        private void CargoUnload(string PlayerId, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, ApplicationDbContext data)
        {
            var transportedResources = ShipsOnMission.Sum(s => s.Storage);
            var playerName = data.Players.First(p => p.Id == PlayerId).UserName;
            var systemId = ShipsOnMission.First().SystemId;
            var systemPosition = data.Systems.First(s => s.Id == systemId).Position;

            foreach (var ship in ShipsOnMission)
            {
                ship.Storage = 0;
            }

            data.Resources.First(r => r.SystemId == destinationSystem.Id && r.Type == ResourceType.MilkyCoin).Quantity += transportedResources;

            var SenderMessageTitle = "Transported resources";
            var SenderMessageContent = $"You've successfully transported {transportedResources} {ResourceType.MilkyCoin.ToString()} to system {destinationSystem.Position}.";

            var RecieverMessageTitle = "Recieved resources";
            var RecieverMessageContent = $"You have recieved {transportedResources} {ResourceType.MilkyCoin.ToString()} by {playerName} from system {systemPosition}.";

            ReportMessage(PlayerId, SenderMessageTitle, SenderMessageContent, data);
            ReportMessage(destinationSystem.PlayerId, RecieverMessageTitle, RecieverMessageContent, data);

        }

        private Fleet Battle(Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, string playerId, string destinationSystemPlayerId, ApplicationDbContext data)
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

                //data.DefensiveStructures.RemoveRange(Defence.Where(d => d.HP <= 0));
                //data.Ships.RemoveRange(DefenderFleet.Where(d => d.HP <= 0));
                //DefenderFleet.ToList().RemoveAll(s => !s.OnMission);
                //Defence.ToList().RemoveAll(d => d.SystemId == destinationSystem.Id); //am I actually removing them?!? 
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
                        var defShipHP = defShip.HP;
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
                        attackerDMG -= defShipHP;
                    }
                }

                if (Defence.Any(d => d.HP <= 0))
                {
                    data.DefensiveStructures.RemoveRange(Defence.Where(d => d.HP <= 0));
                    Defence.ToList().RemoveAll(d => d.HP <= 0);
                }
                if (DefenderFleet.Any(s => s.HP <= 0 && s.Type != ShipType.Colonizer))
                {
                    data.Ships.RemoveRange(DefenderFleet.Where(d => d.HP <= 0 && d.Type != ShipType.Colonizer));
                    DefenderFleet.ToList().RemoveAll(s => s.HP <= 0 && s.Type != ShipType.Colonizer);
                }

            }
            if (defenderDMG >= attackerHP)
            {
                data.Ships.RemoveRange(ShipsOnMission);

                AttackerMessageContent += $"Your {ShipsOnMission.Count} ships were destroyed by the enemy defences, surviving your attack with {Defence.Sum(d => d.HP) + DefenderFleet.Sum(d => d.HP)} HP left.";
                DefenderMessageContent += $"Your defences destroyed all {ShipsOnMission.Count} of the enemy's ships, surviving the attack with {Defence.Sum(d => d.HP) + DefenderFleet.Sum(d => d.HP)} HP left.";

                ShipsOnMission.RemoveAll(s => s.OnMission);
                fleet.ArrivalTime = null;
                ReportMessage(playerId, messageTitle, AttackerMessageContent, data);
                if (destinationSystemPlayerId != null)
                {
                    ReportMessage(destinationSystemPlayerId, messageTitle, DefenderMessageContent, data);
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

                if (ShipsOnMission.Any(s => s.HP <= 0 && s.Type != ShipType.Colonizer))
                {
                    data.Ships.RemoveRange(ShipsOnMission.Where(s => s.HP <= 0 && s.Type != ShipType.Colonizer));
                    ShipsOnMission.RemoveAll(s => s.HP <= 0 && s.Type != ShipType.Colonizer);
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

                ReportMessage(playerId, messageTitle, AttackerMessageContent, data);
                if(destinationSystemPlayerId != null)
                {
                    ReportMessage(destinationSystemPlayerId, messageTitle, DefenderMessageContent, data);
                }
            }

            fleet.Ships = ShipsOnMission;
            return fleet;
        }

        private void ReportMessage(string playerId, string messageTitle, string messageContent, ApplicationDbContext data)
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
            //data.SaveChanges();
        }


        public bool FleetBoarding(string systemId, ApplicationDbContext data)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId && f.ArrivalTime.HasValue).FirstOrDefault(f => f.ArrivalTime.Value <= DateTime.Now);//here

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == system.Id && r.Type == ResourceType.MilkyCoin);

            if (fleet != null)
            {
                var ships = fleet.Ships.ToList();

                foreach (var ship in ships)
                {
                    ship.OnMission = false;
                    ship.FleetId = null;
                    systemMilkyCoin.Quantity += ship.Storage;
                    ship.Storage = 0;
                }

                fleet.ArrivalTime = null;

                //data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

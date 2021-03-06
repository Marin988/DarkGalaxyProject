using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
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
    public class FleetMovement : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public FleetMovement(IServiceScopeFactory scopeFactory)
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

                    if (dbContext.Fleets.Any(f => f.ArrivalTime <= DateTime.Now))
                    {
                        foreach (var fleet in dbContext.Fleets.Where(f => f.ArrivalTime <= DateTime.Now))
                        {
                            if (fleet.Outgoing)
                            {
                                FleetReturn(fleet.SystemId, dbContext, fleet.Id);
                            }
                            else
                            {
                                FleetBoarding(fleet.SystemId, dbContext, fleet.Id);
                            }
                        }
                    }

                    try
                    {
                        await dbContext.SaveChangesAsync();
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"Message: {error.Message}");
                        Console.WriteLine($"Inner exception: {error.InnerException}");
                        Console.WriteLine($"Source: FleetMovement");
                    }

                    await Task.Delay(1000, stoppingToken);
                }
            }
        }

        public bool FleetReturn(string systemId, ApplicationDbContext data, string fleetId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).First(f => f.Id == fleetId);

            string playerId = system.PlayerId;

            if (fleet == null)
            {
                return false;
            }

            fleet.Outgoing = false;

            var destinationSystem = data.Systems.Include(s => s.Ships).Include(s => s.DefensiveStructures).First(s => s.Position == fleet.DestinationSystemPoistion);

            var ShipsOnMission = fleet.Ships.OrderByDescending(s => s.Type).ToList();

            var flightLength = Math.Abs(system.Position - (int)fleet.DestinationSystemPoistion);
            var fleetSpeed = ShipsOnMission.Min(s => s.Speed);

            fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength * 2000 / fleetSpeed);
            fleet.DestinationSystemPoistion = null;

            if (fleet.MissionType == MissionType.Attack)
            {
                Battle(fleet, destinationSystem, ShipsOnMission, playerId, destinationSystem.PlayerId, data);
            }

            if (fleet.MissionType == MissionType.Colonize)
            {
                fleet = Battle(fleet, destinationSystem, ShipsOnMission, playerId, destinationSystem.PlayerId, data);
                ShipsOnMission = fleet.Ships.OrderByDescending(s => s.Type).ToList();
                if (ShipsOnMission.Count > 0)
                {
                    Colonize(playerId, fleet, destinationSystem, ShipsOnMission, data);
                    CargoUnload(playerId, destinationSystem, ShipsOnMission, data, fleet.MissionType);
                }
            }

            if ((fleet.MissionType == MissionType.Transport || fleet.MissionType == MissionType.Deploy) && ShipsOnMission.Sum(s => s.Storage) > 0)
            {
                CargoUnload(playerId, destinationSystem, ShipsOnMission, data, fleet.MissionType);
            }

            if (fleet.MissionType == MissionType.Deploy)
            {
                DeployShips(playerId, fleet, destinationSystem, ShipsOnMission, data);
            }

            if (fleet.MissionType == MissionType.Spy)
            {
                var MessageTitle = $"Spy results on system {destinationSystem.Position}";
                var MessageContent = $"Your espionage probe sent to system {destinationSystem.Position} found {destinationSystem.DefensiveStructures.Count()} defensive structures and {destinationSystem.Ships.Where(s => !s.OnMission).Count()} ships guarding the system.";

                ReportMessage(playerId, MessageTitle, MessageContent, data);
            }

            return true;
        }

        private void Colonize(string playerId, Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, ApplicationDbContext data)
        {
            var colonizer = fleet.Ships.FirstOrDefault(s => s.Type == ShipType.Colonizer);
            string messageTitle;
            string messageContent;

            if (colonizer == null)
            {
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
                ship.PlayerId = destinationSystem.PlayerId;
            }

            if (fleet.MissionType == MissionType.Colonize)
            {
                return;
            }

            var recieverPlayerId = destinationSystem.PlayerId;

            var MessageTitle = $"Deployed ships";
            var recieverMessageContent = $"{ShipsOnMission.Count} ships were deployed to your system {destinationSystem.Position} by {senderUsername} from {systemPosition}";
            var senderMessageContent = $"You deployed {ShipsOnMission.Count} ships to system {destinationSystem.Position}";

            ShipsOnMission.RemoveAll(p => !p.OnMission);

            ReportMessage(senderplayerId, MessageTitle, senderMessageContent, data);

            if (senderplayerId != recieverPlayerId)
            {
                ReportMessage(recieverPlayerId, MessageTitle, recieverMessageContent, data);
            }
        }

        private void CargoUnload(string PlayerId, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, ApplicationDbContext data, MissionType missionType)
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

            if (missionType == MissionType.Colonize)
                return;

            var SenderMessageTitle = "Transported resources";
            var SenderMessageContent = $"You've successfully transported {transportedResources} {ResourceType.MilkyCoin.ToString()} to system {destinationSystem.Position}.";
            ReportMessage(PlayerId, SenderMessageTitle, SenderMessageContent, data);

            if (PlayerId != destinationSystem.PlayerId)
            {
                var RecieverMessageTitle = "Recieved resources";
                var RecieverMessageContent = $"You have recieved {transportedResources} {ResourceType.MilkyCoin.ToString()} by {playerName} from system {systemPosition}.";
                ReportMessage(destinationSystem.PlayerId, RecieverMessageTitle, RecieverMessageContent, data);
            }

        }

        private Fleet Battle(Fleet fleet, Data.Models.System destinationSystem, List<Ship> ShipsOnMission, string playerId, string destinationSystemPlayerId, ApplicationDbContext data)
        {
            var attackerSystemPosition = data.Systems.First(s => s.Id == fleet.SystemId).Position;

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

                AttackerMessageContent += $"Attack on system {destinationSystem.Position} successful.{DefenderFleet.Count()} ships and {Defence.Count()} defensive structures were destroyed.";
                DefenderMessageContent += $"You were attacked by {ShipsOnMission.Count} ships coming from planet {attackerSystemPosition}.All of your {DefenderFleet.Count()} ships and {Defence.Count()} defensive structures were destroyed.";
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
                if (DefenderFleet.Any(s => s.HP <= 0))
                {
                    data.Ships.RemoveRange(DefenderFleet.Where(d => d.HP <= 0));
                    DefenderFleet.ToList().RemoveAll(s => s.HP <= 0);
                }

            }
            if (defenderDMG >= attackerHP)
            {
                data.Ships.RemoveRange(ShipsOnMission);
                data.DefensiveStructures.RemoveRange(Defence.Where(d => d.HP <= 0));
                Defence.ToList().RemoveAll(d => d.HP <= 0);

                AttackerMessageContent += $"Your {ShipsOnMission.Count} ships were destroyed by the enemy defences, {Defence.Count(d => d.HP > 0)} defences and {DefenderFleet.Count(d => d.HP > 0)} ships, surviving your attack with a total of {Defence.Where(d => d.HP > 0).Sum(d => d.HP) + DefenderFleet.Where(d => d.HP > 0).Sum(d => d.HP)} HP left.";
                DefenderMessageContent += $"Your defences destroyed all {ShipsOnMission.Count} of the enemy's ships, {Defence.Count(d => d.HP > 0)} defences and {DefenderFleet.Count(d => d.HP > 0)} ships survived the attack with {Defence.Where(d => d.HP > 0).Sum(d => d.HP) + DefenderFleet.Where(d => d.HP > 0).Sum(d => d.HP)} HP left.";

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
                foreach (var ship in ShipsOnMission.OrderByDescending(s => s.Type).ThenBy(s => s.Storage))
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
                    data.Ships.RemoveRange(ShipsOnMission.Where(s => s.HP <= 0));
                    ShipsOnMission.RemoveAll(s => s.HP <= 0);
                }
            }

            if (ShipsOnMission.Any())
            {
                var systemResource = data.Resources.First(r => r.SystemId == destinationSystem.Id && r.Type == ResourceType.MilkyCoin);
                var loot = 0;

                foreach (var ship in ShipsOnMission)
                {
                    var availableStorage = ship.MaxStorage - ship.Storage;

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

                AttackerMessageContent += $"The enemy defences were destroyed.";
                if(fleet.MissionType == MissionType.Attack)
                {
                    AttackerMessageContent += $" Your fleet looted {loot} {systemResource.Type.ToString()}s and is coming back.";
                }
                DefenderMessageContent += $"The enemy ships looted {loot} {systemResource.Type.ToString()}s.";

                ReportMessage(playerId, messageTitle, AttackerMessageContent, data);
                if (destinationSystemPlayerId != null)
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
                Title = messageTitle,
                Content = messageContent,
                Seen = false
            };

            data.Messages.Add(reportMessage);
        }


        public bool FleetBoarding(string systemId, ApplicationDbContext data, string fleetId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).First(f => f.Id == fleetId);

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

                return true;
            }

            return false;
        }
    }
}

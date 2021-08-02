using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models;
using DarkGalaxyProject.Models.Planet;
using DarkGalaxyProject.Models.System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DarkGalaxyProject.BackgroundTasks;

namespace DarkGalaxyProject.Controllers
{
    public class SystemController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;

        public SystemController(ApplicationDbContext data, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Authorize]
        public IActionResult ViewSystem(string Id)
        {
            Console.WriteLine(data.Systems.Count());

            var system = data.Systems.Where(s => s.Id == Id).Select(s => new SystemViewModel
            {
                Id = s.Id,
                PlayerId = s.PlayerId,
                UserName = s.Player.UserName,
                Position = s.Position,
                Type = s.Type.ToString(),
                Ships = s.Ships.Select(sh => new ShipViewModel
                {
                    Damage = sh.Damage,
                    HP = sh.HP,
                    Speed = sh.Speed,
                    Storage = sh.Storage,
                    Type = sh.Type.ToString()
                }),
                Planets = s.Planets.Select(p => new PlanetListViewModel
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToList()
            })
            .FirstOrDefault();

            return View(system);
        }

        [Authorize]
        public IActionResult Fleet(string systemId)
        {
            var ships = data.Ships
                .Where(s => s.SystemId == systemId)
                .Select(s => new ShipViewModel
                {
                    HP = s.HP,
                    MaxHP = s.MaxHP,
                    Damage = s.Damage,
                    Speed = s.Speed,
                    MaxStorage = s.MaxStorage,
                    Storage = s.Storage,
                    Type = s.Type.ToString(),
                    OnMission = s.OnMission,
                    DealId = s.DealId
                })
                .ToList();

            bool colonise = ships.FirstOrDefault(s => s.Type == ShipType.Colonizer.ToString()) != null;

            var fleets = data.Fleets
                .Where(f => f.SystemId == systemId)
                .Select(f => new FleetFormModel
                {
                    ArrivalTime = f.ArrivalTime,
                    Outgoing = f.Outgoing,
                    Ships = f.Ships.Select(s => new ShipViewModel
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

            var fleet = data.Systems
                .Where(s => s.Id == systemId)
                .Select(s => new FleetViewFormModel
                {
                    Ships = ships,
                    Fleets = fleets,
                    HostSystemId = s.Id,
                    HostSystemPosition = s.Position,
                    PlayerId = s.PlayerId,
                })
                .First();

            return View(fleet);
        }

        [Authorize]
        public IActionResult AllSystems()
        {
            var systems = this.data
                .Systems
                .Select(s => new SystemViewModel
                {
                    Id = s.Id,
                    Position = s.Position,
                    Type = s.Type.ToString(),
                    Ships = s.Ships.Select(sh => new ShipViewModel
                    {
                        Damage = sh.Damage,
                        HP = sh.HP,
                        Speed = sh.Speed,
                        Storage = sh.Storage,
                        Type = sh.Type.ToString()
                    })
                    .ToList(),
                    Planets = s.Planets.Select(p => new PlanetListViewModel
                    {
                        Id = p.Id,
                        Name = p.Name
                    })
                    .ToList()
                })
                .ToList();

            return View(systems);
        }

        [Authorize]
        public IActionResult PlayerSystems(string PlayerId)
        {
            var systems = data.Systems
               .Where(s => s.PlayerId == PlayerId)
               .Select(s => new SystemViewModel
               {
                   Id = s.Id,
                   Position = s.Position,
                   Type = s.Type.ToString(),
                   Ships = s.Ships.Where(sh => !sh.OnMission).Select(sh => new ShipViewModel
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

            return View(systems);
        }

        [Authorize]
        public IActionResult Shipyard(string systemId)
        {
            var shipBuilders = data.ShipBuilders
                .Where(sp => sp.SystemId == systemId)
                .Select(sp => new BuildShipFormViewModel
                {
                    BuildTime = sp.BuildTime,
                    FinishedBuildingTime = sp.FinishedBuildingTime,
                    systemId = sp.SystemId,
                    ShipType = sp.ShipType.ToString(),
                    Count = sp.Count
                })
                .ToList();

            return View(shipBuilders);
        }

        [Authorize]
        public IActionResult DefensiveStructureBuilder(string systemId)
        {
            var defenceBuilders = data.DefenceBuilders
                .Where(sp => sp.SystemId == systemId)
                .Select(sp => new BuildDefenceFormViewModel
                {
                    BuildTime = sp.BuildTime,
                    FinishedBuildingTime = sp.FinishedBuildingTime,
                    systemId = sp.SystemId,
                    DefenceType = sp.DefensiveStructureType.ToString(),
                    Count = sp.Count
                })
                .ToList();

            return View(defenceBuilders);
        }

        [Authorize]
        [HttpPost]
        public IActionResult SwitchSystem(string systemId)
        {
            //var targetedSystem = data.Systems.First(s => s.Id == systemId);

            var player = data.Players.First(p => p.Id == userManager.GetUserId(User));

            player.CurrentSystemId = systemId;

            data.SaveChanges();

            return Redirect($"ViewSystem/{systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Colonize(string systemId)
        {
            var targetedSystem = data.Systems.First(s => s.Id == systemId);

            targetedSystem.PlayerId = userManager.GetUserId(User);

            data.SaveChanges();

            return Redirect($"PlayerSystems?PlayerId={userManager.GetUserId(User)}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendFleet(int battleShipCount, int colonizerCount, int transportShipCount, string missionType, int cargo, int destinationSystemPosition, string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId).First(f => f.ArrivalTime == null);

            var missionTypeEnum = (MissionType)Enum.Parse(typeof(MissionType), missionType);

            //add error if destinationSystemPosition == the current ssytemPosition

            var ships = new List<Ship>();

            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.BattleShip && s.FleetId == null).Take(battleShipCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Colonizer && s.FleetId == null).Take(colonizerCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.TransportShip && s.FleetId == null).Take(transportShipCount));

            if (ships.Count > 0)//else add error
            {
                if (missionTypeEnum == MissionType.Colonize)
                {
                    if (!system.Ships.Any(s => s.Type == ShipType.Colonizer && s.FleetId == null))
                    {
                        //TODO: add error to error list
                        Console.WriteLine("return error view...");
                    }
                    if(data.Systems.First(s => s.Position == destinationSystemPosition).PlayerId != null)
                    {
                        //TODO: add error to error list
                    }
                    //if destinationSystemPosition doesn't exist - add another error
                    if (!ships.Any(s => s.Type == ShipType.Colonizer))
                    {
                        ships.Add(system.Ships.First(s => s.Type == ShipType.Colonizer && s.FleetId == null));//or return error
                    }
                    //TODO: if error list.Count > 0 return error view
                }

                //if deploy check if the system you are deploying to has a playerid - if not - add error

                if (missionTypeEnum == MissionType.Transport && cargo > 0)
                {
                    system.Resources.First(r => r.Type == ResourceType.MilkyCoin).Quantity -= cargo;
                }

                foreach (var ship in ships)
                {
                    ship.OnMission = true;
                    ship.FleetId = fleet.Id;

                    if (missionTypeEnum == MissionType.Transport && cargo > 0) //possible issue - If there are ships already loaded they might
                    {                                                         //be the ones i get in the fleet UNLESS I order them by cargo 
                        int load = ship.MaxStorage - ship.Storage;
                        if (cargo < load)
                        {
                            load = cargo;
                        }

                        ship.Storage += load;
                        cargo -= load;
                    }
                }//issue note (above next to if)

                fleet.MissionType = missionTypeEnum;

                fleet.Outgoing = true;
                fleet.Ships.ToList().AddRange(ships);
                fleet.DestinationSystemPoistion = destinationSystemPosition;

                var flightLength = Math.Abs(system.Position - destinationSystemPosition);

                fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);

                data.SaveChanges();
            }//else add error

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetReturn(string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId && f.ArrivalTime.HasValue).FirstOrDefault(f => f.ArrivalTime.Value <= DateTime.Now);

            if(fleet != null)
            {
                fleet.Outgoing = false;

                var destinationSystem = data.Systems.Include(s => s.Ships).Include(s => s.DefensiveStructures).First(s => s.Position == fleet.DestinationSystemPoistion);

                var ShipsOnMission = fleet.Ships.ToList();

                var flightLength = Math.Abs(system.Position - (int)fleet.DestinationSystemPoistion);
                fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);
                fleet.DestinationSystemPoistion = null;

                if (fleet.MissionType == MissionType.Attack)
                {
                    var attackerHP = ShipsOnMission.Sum(s => s.HP);
                    var attackerDMG = ShipsOnMission.Sum(s => s.Damage);

                    var DefenderFleet = destinationSystem.Ships.Where(s => !s.OnMission);
                    var Defence = destinationSystem.DefensiveStructures;

                    var defenderHP = DefenderFleet.Sum(f => f.HP) + Defence.Sum(d => d.HP);
                    var defenderDMG = DefenderFleet.Sum(f => f.Damage) + Defence.Sum(d => d.Damage);

                    if (attackerDMG >= defenderHP)
                    {
                        DefenderFleet.ToList().RemoveAll(s => !s.OnMission);
                        Defence.ToList().RemoveAll(d => d.SystemId == destinationSystem.Id); //am I actually removing them?!? If so, am I just saying systemId = null or actually deleting them
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
                        ShipsOnMission.RemoveAll(s => s.OnMission);
                        fleet.ArrivalTime = null;
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

                        foreach (var ship in ShipsOnMission)
                        {
                            var availableStorage = ship.MaxStorage - ship.Storage; //may include that as a property

                            if (ship.Storage < ship.MaxStorage)
                            {
                                if (systemResource.Quantity >= availableStorage)
                                {
                                    ship.Storage = ship.MaxStorage;
                                    systemResource.Quantity -= availableStorage;
                                }
                                else
                                {
                                    ship.Storage += systemResource.Quantity;
                                    systemResource.Quantity = 0;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (fleet.MissionType == MissionType.Transport)//I may have a problem here if before adding the amount to be transported, the ships already had  
                {                                             //resources.But I would rather remove storing resources to stationary ships as an option at all
                    var transportedResources = ShipsOnMission.Sum(s => s.Storage);

                    foreach (var ship in ShipsOnMission)
                    {
                        ship.Storage = 0;
                    }

                    destinationSystem.Resources.First(r => r.Type == ResourceType.MilkyCoin).Quantity += transportedResources;
                }


                if (fleet.MissionType == MissionType.Deploy)//I may add transport function in here later
                {
                    fleet.ArrivalTime = null;
                    foreach (var ship in ShipsOnMission)
                    {
                        ship.SystemId = destinationSystem.Id;
                        ship.OnMission = false;
                        ship.PlayerId = destinationSystem.PlayerId;//check if dest system has a playerId if you let that be acceptable in your business logic
                    }
                    ShipsOnMission.RemoveAll(p => !p.OnMission);
                }//NOTE


                if (fleet.MissionType == MissionType.Colonize)//I may add transport function in here later
                {
                    var colonizer = fleet.Ships.First(s => s.Type == ShipType.Colonizer);
                    if (ShipsOnMission.Count == 1)
                    {
                        fleet.ArrivalTime = null;
                    }
                    destinationSystem.PlayerId = userManager.GetUserId(User);
                    data.Ships.Remove(colonizer);
                }//NOTE

                data.SaveChanges();
            }

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetBoarding(string systemId)
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
            }

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartBuilding(string systemId, string shipType, int count)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);
            systemMilkyCoin.Quantity -= (int)shiptype * 500 * count;

            var shipbuildingQueue = system.ShipBuildingQueue.FirstOrDefault(s => s.ShipType == shiptype);

            shipbuildingQueue.Count = count;
            shipbuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(shipbuildingQueue.BuildTime);

            data.SaveChanges();

            return Redirect($"Shipyard?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartBuildingDefence(string systemId, string defenceType, int count)
        {
            var system = data.Systems.Include(s => s.DefenceBuildingQueue).First(s => s.Id == systemId);

            var defencetype = (DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), defenceType);

            var systemMilkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);
            systemMilkyCoin.Quantity -= (int)defencetype * 100 * count;

            var defenceBuildingQueue = system.DefenceBuildingQueue.First(s => s.DefensiveStructureType == defencetype);

            defenceBuildingQueue.Count = count;

            defenceBuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(defenceBuildingQueue.BuildTime);

            data.SaveChanges();

            return Redirect($"DefenceStructureBuilder?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuildShip(string systemId, string shipType)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var shipBuildingQueue = system.ShipBuildingQueue.First(s => s.ShipType == shiptype);

            shipBuildingQueue.FinishedBuildingTime = null;

            for (int i = 0; i < shipBuildingQueue.Count; i++)
            {
                data.Ships.Add(new Ship(shiptype, systemId, userManager.GetUserId(User)));
            }

            shipBuildingQueue.Count = 0;

            data.SaveChanges();

            return Redirect($"Shipyard?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuildDefence(string systemId, string defenceType)
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

            return Redirect($"DefenceStructureBuilder?systemId={systemId}");
        }

    }
}

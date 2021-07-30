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
                    OnMission = s.OnMission
                })
                .ToList();

            bool colonize = ships.FirstOrDefault(s => s.Type == ShipType.Colonizer.ToString()) != null;

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
        public IActionResult Colonize(string systemId)
        {
            var targetedSystem = data.Systems.First(s => s.Id == systemId);

            targetedSystem.PlayerId = userManager.GetUserId(User);

            data.SaveChanges();

            return Redirect($"PlayerSystems?PlayerId={userManager.GetUserId(User)}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendFleet(int goliathCount, int vengeanceCount, int leonovCount, bool colonizer, int destinationSystemPosition, string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Include(f => f.Ships).Where(f => f.SystemId == systemId).First(f => f.ArrivalTime == null);

            var ships = new List<Ship>();

            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Goliath && s.FleetId == null).Take(goliathCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Vengeance && s.FleetId == null).Take(vengeanceCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Leonov && s.FleetId == null).Take(leonovCount));

            if (colonizer)
            {
                var colonizerShip = system.Ships.FirstOrDefault(s => s.Type == ShipType.Colonizer);
                if (colonizerShip != null)
                {
                    ships.Add(colonizerShip);
                }
            }

            if (ships.Count > 0)
            {
                foreach (var ship in ships)
                {
                    ship.OnMission = true;
                    ship.FleetId = fleet.Id;
                }

                fleet.Outgoing = true;
                //fleet.Ships.ToList().AddRange(ships);
                fleet.DestinationSystemPoistion = destinationSystemPosition;

                var flightLength = Math.Abs(system.Position - destinationSystemPosition);

                fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);

                data.SaveChanges();
            }

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetReturn(string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var fleet = data.Fleets.Where(f => f.SystemId == systemId && f.ArrivalTime.HasValue).FirstOrDefault(f => f.ArrivalTime.Value <= DateTime.Now);//here

            if (fleet != null)
            {

                fleet.Outgoing = false;

                var colonizer = fleet.Ships.FirstOrDefault(s => s.Type == ShipType.Colonizer);
                var destinationSystem = data.Systems.First(s => s.Position == fleet.DestinationSystemPoistion);

                var ShipsOnMission = fleet.Ships.ToList();

                var ShipsOnMissionWithoutFullStorage = fleet.Ships.Where(s => s.Storage < s.MaxStorage).ToList();

                //int systemLoot = destinationSystem.Resources.First(p => p.Type == ResourceType.MilkyCoin).Quantity;.Include(s => s.Resources)
                var systemLoot = data.Resources.First(r => r.SystemId == destinationSystem.Id && r.Type == ResourceType.MilkyCoin);

                foreach (var ship in ShipsOnMissionWithoutFullStorage)
                {
                    var diff = ship.MaxStorage - ship.Storage;
                    if (systemLoot.Quantity == 0)//bad logic
                    {
                        break;
                    }
                    if (systemLoot.Quantity <= diff)//bad logic
                    {
                        ship.Storage += systemLoot.Quantity;
                    }
                    else//bad logic
                    {
                        ship.Storage = ship.MaxStorage;
                    }
                    systemLoot.Quantity -= diff;
                }

                var flightLength = Math.Abs(system.Position - (int)fleet.DestinationSystemPoistion);

                fleet.ArrivalTime = DateTime.Now.AddSeconds(flightLength);

                fleet.DestinationSystemPoistion = null;

                if (colonizer != null && ShipsOnMission.Count == 1)
                {
                    fleet.ArrivalTime = null;
                }

                if (colonizer != null)
                {
                    destinationSystem.PlayerId = userManager.GetUserId(User);
                    data.Ships.Remove(colonizer);
                }

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

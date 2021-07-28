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
                PlayerId = s.UserId,
                UserName = s.User.UserName,
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

            var fleet = data.Systems
                .Where(s => s.Id == systemId)
                .Select(s => new FleetViewFormModel
                {
                    Ships = ships,
                    HostSystemId = s.Id,
                    HostSystemPosition = s.Position,
                    DestinationSystemPosition = s.DestinationSystemPoistion,
                    ArrivalTime = s.ArrivalTime,
                    DepartureTime = s.DepartureTime,
                    Outgoing = s.Outgoing,
                    PlayerId = s.UserId,
                    Colonizer = colonize
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
               .Where(s => s.UserId == PlayerId)
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

            targetedSystem.UserId = userManager.GetUserId(User);

            data.SaveChanges();

            return Redirect($"PlayerSystems?PlayerId={userManager.GetUserId(User)}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendFleet(int goliathCount, int vengeanceCount, int leonovCount, bool colonizer, int destinationSystemPosition, string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var ships = new List<Ship>();

            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Goliath).Take(goliathCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Vengeance).Take(vengeanceCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Leonov).Take(leonovCount));

            if (colonizer)
            {
                ships.Add(system.Ships.FirstOrDefault(s => s.Type == ShipType.Colonizer));
            }

            foreach (var ship in ships)
            {
                ship.OnMission = true;
            }

            system.Outgoing = true;

            system.DestinationSystemPoistion = destinationSystemPosition;

            var flightLength = Math.Abs(system.Position - destinationSystemPosition);

            system.DepartureTime = DateTime.Now;
            system.ArrivalTime = system.DepartureTime.Value.AddSeconds(flightLength);

            data.SaveChanges();

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetReturn(string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            system.Outgoing = false;

            var colonizer = system.Ships.Where(s => s.OnMission).FirstOrDefault(s => s.Type == ShipType.Colonizer);
            var destinationSystem = data.Systems.FirstOrDefault(s => s.Position == system.DestinationSystemPoistion);

            var ships = system.Ships.Where(s => s.OnMission && s.Storage < s.MaxStorage).ToList(); //this would havef to be changed if there is more than 1 fleet

            //int systemLoot = destinationSystem.Resources.First(p => p.Type == ResourceType.MilkyCoin).Quantity;.Include(s => s.Resources)
            var systemLoot = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin);

            foreach (var ship in ships)
            {
                var diff = ship.MaxStorage - ship.Storage;
                if(systemLoot.Quantity == 0)
                {
                    break;
                }
                if (systemLoot.Quantity <= diff)
                {
                    ship.Storage += systemLoot.Quantity;
                }
                else
                {
                    ship.Storage = ship.MaxStorage;
                }
                systemLoot.Quantity -= diff;
            }

            if (colonizer != null)
            {
                destinationSystem.UserId = userManager.GetUserId(User);
            }

            var flightLength = Math.Abs(system.Position - (int)system.DestinationSystemPoistion);

            system.DepartureTime = DateTime.Now;
            system.ArrivalTime = system.DepartureTime.Value.AddSeconds(flightLength);

            system.DestinationSystemPoistion = null;

            data.SaveChanges();

            if(colonizer != null)
            {
                data.Ships.Remove(colonizer);
            }

            data.SaveChanges();

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetBoarding(string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var shipsOnMission = system.Ships.Where(s => s.OnMission).ToList();

            foreach (var ship in shipsOnMission)
            {
                ship.OnMission = false;
            }

            system.ArrivalTime = null;
            system.DepartureTime = null;

            data.SaveChanges();

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartBuilding(string systemId, string shipType, int count)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var playerMilkyCoin = data.Resources.First(r => r.PlayerId == userManager.GetUserId(User) && r.Type == ResourceType.MilkyCoin);
            playerMilkyCoin.Quantity -= (int)shiptype * 500 * count;

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

            var playerMilkyCoin = data.Resources.First(r => r.PlayerId == userManager.GetUserId(User) && r.Type == ResourceType.MilkyCoin);
            playerMilkyCoin.Quantity -= (int)defencetype * 100 * count;

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

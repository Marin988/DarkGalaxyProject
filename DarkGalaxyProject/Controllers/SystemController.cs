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
                    Outgoing = s.Outgoing
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
        [HttpPost]
        public IActionResult Colonize(string systemId)
        {
            var targetedSystem = data.Systems.First(s => s.Id == systemId);

            targetedSystem.UserId = userManager.GetUserId(User);

            data.SaveChanges();

            return Redirect($"PlayerSystems?PlayerId={userManager.GetUserId(User)}");
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult BuildShip(SystemViewModel badPractice)
        //{
        //    data.Ships.Add(new Ship((ShipType)Enum.Parse(typeof(ShipType), badPractice.Type), badPractice.Id)
        //    {
        //        PlayerId = userManager.GetUserId(User)
        //    });

        //    data.SaveChanges();

        //    return Redirect($"ViewSystem/{badPractice.Id}");
        //}

        [Authorize]
        [HttpPost]
        public IActionResult BuildDefence(SystemViewModel badPractice)
        {
            data.DefensiveStructures.Add(new DefensiveStructure((DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), badPractice.Type), badPractice.Id));

            data.SaveChanges();

            return Redirect($"ViewSystem/{badPractice.Id}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult SendFleet(int goliathCount, int vengeanceCount, int leonovCount, int destinationSystemPosition, string systemId)
        {
            var system = data.Systems.Include(s => s.Ships).First(s => s.Id == systemId);

            var ships = new List<Ship>();

            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Goliath).Take(goliathCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Vengeance).Take(vengeanceCount));
            ships.AddRange(system.Ships.Where(s => s.Type == ShipType.Leonov).Take(leonovCount));

            foreach (var ship in ships)
            {
                ship.OnMission = true;
            }

            system.Outgoing = true;

            var flightLength = Math.Abs(system.Position - destinationSystemPosition);

            Console.WriteLine("Send fleet dep time" + system.DepartureTime);

            system.DepartureTime = DateTime.Now;
            system.ArrivalTime = system.DepartureTime.Value.AddSeconds(flightLength);

            Console.WriteLine("Send fleet dep time" + system.DepartureTime);

            data.SaveChanges();

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult FleetReturn(string systemId, int destinationSystemPosition)
        {
            var system = data.Systems.First(s => s.Id == systemId);

            system.Outgoing = false;

            var flightLength = Math.Abs(system.Position - destinationSystemPosition);

            system.DepartureTime = DateTime.Now;
            system.ArrivalTime = system.DepartureTime.Value.AddSeconds(flightLength);

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

            Console.WriteLine($"before nullifying - {system.ArrivalTime}");

            system.ArrivalTime = null;
            system.DepartureTime = null;

            Console.WriteLine($"after nullifying - {system.ArrivalTime}");

            data.SaveChanges();

            return Redirect($"Fleet?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult StartBuilding(string systemId, string shipType, int count)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var shipbuildingQueue = system.ShipBuildingQueue.First(s => s.ShipType == shiptype);

            shipbuildingQueue.Count = count;
            shipbuildingQueue.FinishedBuildingTime = DateTime.Now.AddSeconds(shipbuildingQueue.BuildTime);

            data.SaveChanges();

            return Redirect($"Shipyard?systemId={systemId}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuildShip(string systemId, string shipType)
        {
            var system = data.Systems.Include(s => s.ShipBuildingQueue).First(s => s.Id == systemId);

            var shiptype = (ShipType)Enum.Parse(typeof(ShipType), shipType);

            var shipBuildingQueue = system.ShipBuildingQueue.First(s => s.ShipType == shiptype);

            Console.WriteLine("----------------------------------------");
            Console.WriteLine(shipType);
            Console.WriteLine(data.ShipBuilders.First(s => s.SystemId == systemId && s.ShipType == shiptype).FinishedBuildingTime);
            Console.WriteLine(data.ShipBuilders.First(s => s.SystemId == systemId && s.ShipType == shiptype).Count);

            Console.WriteLine("---------------------AFTER-------------------");

            //2 vengeances are still on queue and the page doesn't stop reloading

            shipBuildingQueue.FinishedBuildingTime = null;

            for (int i = 0; i < shipBuildingQueue.Count; i++)
            {
                data.Ships.Add(new Ship(shiptype, systemId, userManager.GetUserId(User)));
            }

            shipBuildingQueue.Count = 0;

            Console.WriteLine(shipType);
            Console.WriteLine(shipBuildingQueue.FinishedBuildingTime);
            Console.WriteLine(shipBuildingQueue.Count);

            Console.WriteLine("----------------------------------------");

            data.SaveChanges();

            return Redirect($"Shipyard?systemId={systemId}");
        }

    }
}

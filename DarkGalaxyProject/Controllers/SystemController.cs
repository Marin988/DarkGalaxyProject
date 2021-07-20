using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                })
            })
            .FirstOrDefault();

            return View(system);
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
                    EnergyPlanet = new EnergyPlanetViewModel
                    {
                        Name = s.EnergyPlanet.Name,
                        Position = s.EnergyPlanet.Position,
                        Type = s.EnergyPlanet.Type.ToString(),
                        FuelToEnergyCenter = new ResourceBuildingViewModel
                        {
                            Level = s.EnergyPlanet.FuelToEnergyCenter.Level
                        }
                    }
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
                   Ships = s.Ships.Select(sh => new ShipViewModel
                   {
                       Damage = sh.Damage,
                       HP = sh.HP,
                       Speed = sh.Speed,
                       Storage = sh.Storage,
                       Type = sh.Type.ToString()
                   })
                   .ToList(),
                   EnergyPlanet = new EnergyPlanetViewModel
                   {
                       Name = s.EnergyPlanet.Name,
                       Position = s.EnergyPlanet.Position,
                       Type = s.EnergyPlanet.Type.ToString(),
                       FuelToEnergyCenter = new ResourceBuildingViewModel
                       {
                           Level = s.EnergyPlanet.FuelToEnergyCenter.Level
                       }
                   }
               })
               .ToList();

            return View(systems);
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
        public IActionResult BuildShip(SystemViewModel badPractice)
        {
            data.Ships.Add(new Ship((ShipType)Enum.Parse(typeof(ShipType), badPractice.Type), badPractice.Id)
            {
                PlayerId = userManager.GetUserId(User)
            });

            data.SaveChanges();

            return Redirect($"ViewSystem/{badPractice.Id}");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BuildDefence(SystemViewModel badPractice)
        {
            data.DefensiveStructures.Add(new DefensiveStructure((DefensiveStructureType)Enum.Parse(typeof(DefensiveStructureType), badPractice.Type), badPractice.Id));

            data.SaveChanges();

            return Redirect($"ViewSystem/{badPractice.Id}");
        }

    }
}

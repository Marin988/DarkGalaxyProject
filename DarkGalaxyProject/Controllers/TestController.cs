using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly SignInManager<Player> signInManager;
        //private readonly RoleManager<Player> userManager;

        public TestController(ApplicationDbContext data, UserManager<Player> userManager, SignInManager<Player> signInManager)
        {
            this.data = data;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        //[Authorize]
        //public IActionResult AllianceChat()
        //{
        //    var currentUserId = userManager.GetUserId(User);
        //    var currentUser = userManager.Users.First(u => u.Id == currentUserId);

        //    //if there are no alliances i get error

        //    var republicChat = data.ChatMessages.Where(cm => cm.RepublicId != null)
        //        .Select(cm => new AllianceChatMessageViewModel
        //        {
        //            AllianceId = cm.RepublicId,
        //            Content = cm.Content,
        //            Sender = cm.Player.UserName,
        //            SenderId = cm.PlayerId,
        //            TimeOfSending = cm.TimeOfSending
        //        })
        //        .ToList();

        //    var chat = new AllianceChatViewModel
        //    {
        //        Messages = republicChat,
        //        AllianceId = currentUser.RepublicId,
        //        SenderId = currentUserId,
        //        Sender = currentUser.UserName
        //    };

        //    return View(chat);
        //}

        [Authorize]
        public IActionResult All()
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
                    .ToList()
                })
                .ToList();

            return View(systems);
        }

        [Authorize]
        public IActionResult PlayerSystems()
        {
            var systems = data.Systems
                .Where(s => s.UserId == userManager.GetUserId(User))
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
                    .ToList()
                })
                .ToList();

            return View(systems);
        }

        [Authorize]
        //public IActionResult AllianceList()
        //{
        //    var federations = data.Federations
        //        .Select(f => new AllianceViewModel
        //        {
        //            Name = f.Name,
        //            Type = f.GetType().Name,
        //            MembersCount = f.Members.Count()
        //        })
        //        .ToList();

        //    var authoritorians = data.Authoritarians
        //        .Select(a => new AllianceViewModel
        //        {
        //            Name = a.Name,
        //            LeaderName = a.Leader.UserName,
        //            Type = a.GetType().Name,
        //            MembersCount = a.Members.Count()
        //        })
        //        .ToList();

        //    var republics = data.Republics
        //        .Select(r => new AllianceViewModel
        //        {
        //            Name = r.Name,
        //            LeaderName = r.Leader.UserName,
        //            Type = r.GetType().Name,
        //            MembersCount = r.Members.Count()
        //        })
        //        .ToList();

        //    var alliances = new List<AllianceViewModel>();

        //    alliances.AddRange(federations);
        //    alliances.AddRange(authoritorians);
        //    alliances.AddRange(republics);

        //    return View(alliances);
        //}

        //[Authorize]
        //public IActionResult Alliance()
        //{
        //    var alliance = data.Authoritarians
        //        .Select(a => new AllianceViewModel
        //        {
        //            Name = a.Name,
        //            Type = a.Type.ToString(),
        //            LeaderName = a.Leader.UserName,
        //            MembersCount = a.Members.Count()
        //        })
        //        .First();

        //    return View(alliance);
        //}

        [Authorize]
        public IActionResult CreateAlliance()
        {
            return View();
        }

        [Authorize]
        public IActionResult BuildShip()
        {
            return View();
        }

        [Authorize]
        public IActionResult SendShip()
        {
            return View();
        }

        //[Authorize]
        //public IActionResult ViewSystem(string systemId)
        //{
        //    var system = data.Systems.Where(s => s.Id == systemId).Select(s => new SystemViewModel
        //    {
        //        Id = s.Id,
        //        Position = s.Position,
        //        Type = s.Type.ToString(),
        //        EnergyPlanet = new EnergyPlanetViewModel
        //        {
        //            Name = s.Name,
        //            Position = s.Position,
        //            Type = s.Type.ToString(),
        //            FuelToEnergyCenter = new ResourceBuildingViewModel
        //            {
        //                EnergyCost = s.EnergyPlanet.FuelToEnergyCenter.EnergyCost,
        //                Level = s.EnergyPlanet.FuelToEnergyCenter.Level,
        //                Production = s.EnergyPlanet.FuelToEnergyCenter.Production,
        //                Type = s.EnergyPlanet.FuelToEnergyCenter.Type.ToString()
        //            },
        //            SolarPanel = new ResourceBuildingViewModel
        //            {
        //                EnergyCost = s.EnergyPlanet.SolarPanel.EnergyCost,
        //                Level = s.EnergyPlanet.SolarPanel.Level,
        //                Production = s.EnergyPlanet.SolarPanel.Production,
        //                Type = s.EnergyPlanet.SolarPanel.Type.ToString()
        //            },
        //            GeothermalPlant = new ResourceBuildingViewModel
        //            {
        //                EnergyCost = s.EnergyPlanet.GeothermalPlant.EnergyCost,
        //                Level = s.EnergyPlanet.GeothermalPlant.Level,
        //                Production = s.EnergyPlanet.GeothermalPlant.Production,
        //                Type = s.EnergyPlanet.GeothermalPlant.Type.ToString()
        //            }
        //        },
        //        PopulatedPlanet = new PopulatedPlanetViewModel
        //        {
        //            Name = s.PopulatedPlanet.Name,
        //            Position = s.PopulatedPlanet.Position,
        //            Type = s.PopulatedPlanet.Type.ToString(),
        //            Population = s.PopulatedPlanet.Population,
        //            Amenity = new AmenityViewModel
        //            {
        //                Level = s.PopulatedPlanet.Amenities.Level,
        //                CulturalIncrement = s.PopulatedPlanet.Amenities.CultureIncrement,
        //                EnergyCost = s.PopulatedPlanet.Amenities.EnergyCost
        //            },
        //            LivingQuarters = new LivingQuartersViewModel
        //            {
        //                Level = s.PopulatedPlanet.LivingQuarters.Level,
        //                PopulationCapacity = s.PopulatedPlanet.LivingQuarters.PopulationCapacity,
        //                UpgradeCost = s.PopulatedPlanet.LivingQuarters.UpgradeCost
        //            }
        //        },
        //        ResourcePlanet = new ResourcePlanetViewModel
        //        {
        //            Name = s.ResourcePlanet.Name,
        //            Position = s.ResourcePlanet.Position,
        //            Type = s.ResourcePlanet.Type.ToString(),
        //            ResourceBuildings = s.ResourcePlanet.ResourceBuildings.Select(rb => new ResourceBuildingViewModel
        //            {
        //                EnergyCost = rb.EnergyCost,
        //                Level = rb.Level,
        //                Production = rb.Production,
        //                Type = rb.Type.ToString()
        //            }),
        //            StorageBuildings = s.ResourcePlanet.StorageBuildings.Select(sb => new StorageBuildingViewModel
        //            {
        //                EnergyCost = sb.EnergyCost,
        //                Level = sb.Level,
        //                ResourceCapacity = sb.ResourceCapacity,
        //                Type = sb.Type.ToString()
        //            })
        //        },
        //        Ships = s.Ships.Select(sh => new ShipViewModel
        //        {
        //            Damage = sh.Damage,
        //            HP = sh.HP,
        //            Speed = sh.Speed,
        //            Storage = sh.Storage,
        //            Type = sh.Type.ToString()
        //        })
        //    })
        //    .FirstOrDefault();

        //    return View(system);
        //}

        [Authorize]
        [HttpPost]
        public IActionResult Send(AllianceChatViewModel message)
        {
            //data.Republics.First().Messages.ToList().Add(new ChatMessage
            //{
            //    AllianceId = message.AllianceId,
            //    Content = message.Content,
            //    PlayerId = message.SenderId,
            //    Type = ChatMessageType.Chat
            //});

            data.ChatMessages.Add(new ChatMessage
            {
                AllianceId = message.AllianceId,
                Content = message.Content,
                PlayerId = message.SenderId,
                Type = ChatMessageType.Chat
            });

            data.SaveChanges();

            return Redirect("AllianceChat");
        }

        //[Authorize]
        //[HttpPost]
        //public IActionResult CreateAlliance(CreateAllianceViewModel model)
        //{
        //    var currentUser = userManager.Users.First(u => u.Id == userManager.GetUserId(User));

        //    switch (model.Type)
        //    {
        //        case AllianceType.Federation:
        //            var federation = new Federation(model.Name);
        //            federation.Members.ToList().Add(currentUser);
        //            currentUser.FederationId = federation.Id;
        //            data.Federations.Add(federation);
        //            break;
        //        case AllianceType.Authoritorian:
        //            var authoritarian = new Authoritarian(model.Name, userManager.GetUserId(User));
        //            authoritarian.Members.ToList().Add(currentUser);
        //            currentUser.AuthoritorianId = authoritarian.Id;
        //            data.Authoritarians.Add(authoritarian);
        //            break;
        //        case AllianceType.Republic:
        //            var republic = new Republic(model.Name, userManager.GetUserId(User));
        //            republic.Members.ToList().Add(currentUser);
        //            currentUser.RepublicId = republic.Id;
        //            data.Republics.Add(republic);
        //            break;
        //        case AllianceType.OligarchyAuthoritorian:
        //            var oligarchyAuthoritorian = new OligarchyAuthoritarian(model.Name);
        //            oligarchyAuthoritorian.Members.ToList().Add(currentUser);
        //            currentUser.OligarchyAuthoritarianId = oligarchyAuthoritorian.Id;
        //            data.OligarchyAuthoritarians.Add(oligarchyAuthoritorian);
        //            break;
        //        case AllianceType.OligarchyRepublic:
        //            var oligarchyRepublic = new OligarchyRepublic(model.Name);
        //            oligarchyRepublic.Members.ToList().Add(currentUser);
        //            currentUser.OligarchyRepublicId = oligarchyRepublic.Id;
        //            //add leader
        //            data.OligarchyRepublics.Add(oligarchyRepublic);
        //            break;
        //    }

        //    data.SaveChanges();



        //    return Redirect("All");
        //}

        [Authorize]
        [HttpPost]
        public IActionResult Colonize(string systemId)
        {
            var targetedSystem = data.Systems.First(s => s.Id == systemId);

            targetedSystem.UserId = userManager.GetUserId(User);

            data.SaveChanges();

            return Redirect("All");
        }

        [HttpPost]
        [Authorize]
        public IActionResult LevelUp(string name)
        {
            //var fueltoEnergyCenter = data.ResourceBuildings.FirstOrDefault(rb => rb.Planet.Name == name);

            //fueltoEnergyCenter.Level += 1;

            data.SaveChanges();

            return Redirect("All");
        }

        [HttpPost]
        [Authorize]
        public IActionResult SendShip(SendShipViewModel shipModel)
        {
            string systmId = Request.QueryString.ToUriComponent();
            string wtf = Request.Headers["Referer"];

            string systemId = wtf.Split("?")[1];

            var newSystemId = data.Systems.First(s => s.Position == shipModel.SystemCoordinates).Id;

            data.Ships.First(s => s.Type == (ShipType)Enum.Parse(typeof(ShipType), shipModel.Ship) && s.SystemId == systemId).SystemId = newSystemId;

            data.SaveChanges();

            return Redirect("All");
        }

        [HttpPost]
        [Authorize]
        public IActionResult BuildShip(BuildShipViewModel shipModel)
        {
            string systmId = Request.QueryString.ToUriComponent();
            string wtf = Request.Headers["Referer"];

            string systemId = wtf.Split("?")[1];

            var ships = new List<Ship>();

            for (int i = 0; i < shipModel.Quantity; i++)
            {
                var ship = new Ship((ShipType)Enum.Parse(typeof(ShipType), shipModel.Type), systemId);

                ships.Add(ship);
            }

            data.Ships.AddRange(ships);

            data.SaveChanges();

            return Redirect("All");
        }
    }
}

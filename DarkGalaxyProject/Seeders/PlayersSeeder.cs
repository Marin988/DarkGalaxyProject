using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.PlayerServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Seeders
{
    public class PlayersSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext data;
        private readonly UserManager<Player> userManager;
        private readonly IPlayerService players;

        public PlayersSeeder(ApplicationDbContext data, UserManager<Player> userManager, IPlayerService players)
        {
            this.data = data;
            this.userManager = userManager;
            this.players = players;
        }

        public void Seed()
        {
            if (!data.Players.Any())
            {
                Task.Run(async () =>
                {
                    var systemForUser = data.Systems.First(s => s.PlayerId == null && s.Type == SystemType.Large);
                    var systemResources = data.Resources.Where(r => r.SystemId == systemForUser.Id).ToList();



                    foreach (var resource in systemResources)
                    {
                        resource.Quantity = 100000;
                    }


                    var SystemMessagesSender = new Player()
                    {
                        Email = "TheBoss@abv.bg",
                        UserName = "TheBoss",
                        CurrentSystemId = systemForUser.Id,
                        Systems = new List<Data.Models.System>() { systemForUser },
                    };

                    var result = await userManager.CreateAsync(SystemMessagesSender, "a!1jJk09");

                    systemForUser.PlayerId = SystemMessagesSender.Id;
                    var ships = Enumerable.Range(0, 2000).Select(d => new Ship(ShipType.BattleShip, systemForUser.Id, SystemMessagesSender.Id, 40000, 40000, 2000, 250, 300));
                    data.Ships.AddRange(ships);

                    if (!result.Succeeded)
                    {
                        var errors = result.Errors.Select(e => e.Description);

                        foreach (var error in errors)
                        {
                            Console.WriteLine(error);
                        }
                    }

                    players.PlayerResearches(SystemMessagesSender.Id);

                    await data.SaveChangesAsync();
                })
                .GetAwaiter()
                .GetResult();
            }
        }
    }
}

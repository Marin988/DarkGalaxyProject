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
                    var systemForBoss = data.Systems.First(s => s.PlayerId == null && s.Type == SystemType.Large);
                    var systemResourcesBoss = data.Resources.Where(r => r.SystemId == systemForBoss.Id).ToList();

                    var systemForFighter = data.Systems.First(s => s.PlayerId == null && s.Type == SystemType.Medium);
                    var systemResourcesFighter = data.Resources.Where(r => r.SystemId == systemForFighter.Id).ToList();

                    foreach (var resource in systemResourcesFighter)
                    {
                        resource.Quantity = 10000000;
                    }

                    foreach (var resource in systemResourcesBoss)
                    {
                        resource.Quantity = 10000000;
                    }

                    var shipStats = data.ShipStats.First(s => s.Type == ShipType.BattleShip);

                    var theFighter = new Player()
                    {
                        Email = "Fighter@abv.bg",
                        UserName = "Fighter",
                        CurrentSystemId = systemForFighter.Id,
                        Systems = new List<Data.Models.System>() { systemForFighter }
                    };

                    var TheBoss = new Player()
                    {
                        Email = "TheBoss@abv.bg",
                        UserName = "TheBoss",
                        CurrentSystemId = systemForBoss.Id,
                        Systems = new List<Data.Models.System>() { systemForBoss }
                    };

                    var resultBoss = await userManager.CreateAsync(TheBoss, "a!1jJk09");
                    var resultFigher = await userManager.CreateAsync(theFighter, "a!1jJk09");
                    AssignInitialShips(systemForBoss, shipStats, TheBoss, 2000);
                    AssignInitialShips(systemForFighter, shipStats, theFighter, 1000);

                    if (!resultBoss.Succeeded)
                    {
                        var errors = resultBoss.Errors.Select(e => e.Description);

                        foreach (var error in errors)
                        {
                            Console.WriteLine(error);
                        }
                    }

                    players.PlayerResearches(TheBoss.Id);
                    players.PlayerResearches(theFighter.Id);

                    await data.SaveChangesAsync();
                })
                .GetAwaiter()
                .GetResult();
            }
        }

        private void AssignInitialShips(Data.Models.System system, Data.Models.Stats.ShipStats shipStats, Player player, int shipsNumber)
        {
            system.PlayerId = player.Id;
            var ships = Enumerable.Range(0, shipsNumber).Select(d => new Ship(shipStats.Type, system.Id, player.Id, shipStats.Damage, shipStats.MaxHP, shipStats.MaxStorage, shipStats.Speed, shipStats.FuelExpense));
            data.Ships.AddRange(ships);
        }
    }
}

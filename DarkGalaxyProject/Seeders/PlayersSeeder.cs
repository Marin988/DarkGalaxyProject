using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Stats;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.PlayerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
                    var systemForEmperor = data.Systems.Include(s => s.Resources).First(s => s.Position == 50);
                    var systemForFighter = data.Systems.Include(s => s.Resources).First(s => s.Position == 51);

                    var shipStats = data.ShipStats.First(s => s.Type == ShipType.BattleShip);

                    var theFighter = new Player()
                    {
                        Email = "Fighter@abv.bg",
                        UserName = "Fighter",
                        CurrentSystemId = systemForFighter.Id,
                        Systems = new List<Data.Models.System>() { systemForFighter }
                    };
                    var TheEmperor = new Player()
                    {
                        Email = "TheEmperor@abv.bg",
                        UserName = "TheEmperor",
                        CurrentSystemId = systemForEmperor.Id,
                        Systems = new List<Data.Models.System>() { systemForEmperor }
                    };

                    systemForEmperor.CurrentPlayerId = TheEmperor.Id;
                    systemForFighter.CurrentPlayerId = theFighter.Id;

                    var resultBoss = await userManager.CreateAsync(TheEmperor, "a!1jJk09");
                    var resultFigher = await userManager.CreateAsync(theFighter, "a!1jJk09");

                    var systemsForEmperor = data.Systems.Include(s => s.Resources).Where(s => s.PlayerId == null).Take(5).ToList();
                    AssignSystems(TheEmperor.Id, systemsForEmperor);
                    data.SaveChanges();
                    var systemsForFighter = data.Systems.Include(s => s.Resources).Where(s => s.PlayerId == null).Take(2).ToList();
                    AssignSystems(theFighter.Id, systemsForFighter);

                    systemsForEmperor.Add(systemForEmperor);
                    systemsForFighter.Add(systemForFighter);

                    SettingSystemResourcesAndAddingShips(systemsForEmperor, shipStats, TheEmperor.Id, data);
                    SettingSystemResourcesAndAddingShips(systemsForFighter, shipStats, theFighter.Id, data);

                    AssignInitialShips(systemForEmperor, shipStats, TheEmperor.Id, 1000, data);
                    AssignInitialShips(systemForFighter, shipStats, theFighter.Id, 500, data);

                    if (!resultBoss.Succeeded)
                    {
                        var errors = resultBoss.Errors.Select(e => e.Description);

                        foreach (var error in errors)
                        {
                            Console.WriteLine(error);
                        }
                    }

                    players.PlayerResearches(TheEmperor.Id);
                    players.PlayerResearches(theFighter.Id);

                    await data.SaveChangesAsync();
                })
                .GetAwaiter()
                .GetResult();
            }
        }

        private static void AssignSystems(string playerId, List<Data.Models.System> systemsForEmperor)
        {
            foreach (var system in systemsForEmperor)
            {
                system.PlayerId = playerId;
            }
        }

        private static void SettingSystemResourcesAndAddingShips(List<Data.Models.System> systemsForEmperor, ShipStats shipStats, string playerId, ApplicationDbContext data)
        {
            foreach (var system in systemsForEmperor)
            {
                foreach (var resource in system.Resources)
                {
                    resource.Quantity = 10000000;
                }
                AssignInitialShips(system, shipStats, playerId, 50, data);
            }
        }

        private static void AssignInitialShips(Data.Models.System system, ShipStats shipStats, string playerId, int shipsNumber, ApplicationDbContext data)
        {
            system.PlayerId = playerId;
            var ships = Enumerable.Range(0, shipsNumber).Select(d => new Ship(shipStats.Type, system.Id, playerId, shipStats.Damage, shipStats.MaxHP, shipStats.MaxStorage, shipStats.Speed, shipStats.FuelExpense));
            data.Ships.AddRange(ships);
        }
    }
}

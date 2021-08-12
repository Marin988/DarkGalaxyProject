using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
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

        public PlayersSeeder(ApplicationDbContext data, UserManager<Player> userManager)
        {
            this.data = data;
            this.userManager = userManager;
        }

        public void Seed()
        {
            return;
        }

        public async Task SeedUsers()
        {
            if (!data.Players.Any())
            {
                var SystemMessagesSender = new Player()
                {
                    Email = "SystemMessagesSender@abv.bg",
                    UserName = "System",
                    CurrentSystemId = data.Systems.First(s => s.Position == 1).Id
                };

                var result = await userManager.CreateAsync(SystemMessagesSender, "a!1jJk09");

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => e.Description);

                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);
                    }
                }

                await data.SaveChangesAsync();
            }
        }
    }
}

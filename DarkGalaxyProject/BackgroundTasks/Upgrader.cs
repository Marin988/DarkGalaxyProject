using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models.WithinSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DarkGalaxyProject.BackgroundTasks
{
    public class Upgrader : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public Upgrader(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    if (dbContext.Factories.Any(f => f.UpgradeFinishTime <= DateTime.Now))
                    {
                        BuildFactories(dbContext);
                    }

                    if (dbContext.ShipBuilders.Any(s => s.FinishedBuildingTime <= DateTime.Now))
                    {
                        BuildShips(dbContext);
                    }

                    if (dbContext.DefenceBuilders.Any(d => d.FinishedBuildingTime <= DateTime.Now))
                    {
                        BuildDefences(dbContext);
                    }

                    try
                    {
                        await dbContext.SaveChangesAsync();
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"Message: {error.Message}");
                        Console.WriteLine($"Inner exception: {error.InnerException}");
                        Console.WriteLine($"Source: Upgrader");
                    }

                    //await dbContext.SaveChangesAsync();//cancel token None - read about it

                    await Task.Delay(1000, stoppingToken);
                }
            }
        }

        private static void BuildFactories(ApplicationDbContext dbContext)
        {
            foreach (var factory in dbContext.Factories.Where(f => f.UpgradeFinishTime <= DateTime.Now))
            {
                factory.Level += 1;
                factory.UpgradeFinishTime = null;
            }
        }

        private static void BuildDefences(ApplicationDbContext dbContext)
        {
            List<DefensiveStructure> defences = new List<DefensiveStructure>();
            foreach (var defenceBuilder in dbContext.DefenceBuilders.Where(d => d.FinishedBuildingTime <= DateTime.Now))
            {
                for (int i = 0; i < defenceBuilder.Count; i++)
                {
                    defences.Add(new DefensiveStructure(defenceBuilder.DefensiveStructureType, defenceBuilder.SystemId));
                }

                defenceBuilder.FinishedBuildingTime = null;
                defenceBuilder.Count = 0;
            }

            dbContext.AddRange(defences);
        }

        private static void BuildShips(ApplicationDbContext dbContext)
        {
            List<Ship> ships = new List<Ship>();

            foreach (var shipBuilder in dbContext.ShipBuilders.Where(s => s.FinishedBuildingTime <= DateTime.Now))
            {
                var playerId = dbContext.Systems.First(s => s.Id == shipBuilder.SystemId).PlayerId;

                for (int i = 0; i < shipBuilder.Count; i++)
                {
                    ships.Add(new Ship(shipBuilder.ShipType, shipBuilder.SystemId, playerId));
                }

                shipBuilder.FinishedBuildingTime = null;
                shipBuilder.Count = 0;
            }

            dbContext.AddRange(ships);
        }
    }
}

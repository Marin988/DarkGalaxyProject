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

                    await Task.Delay(1000, stoppingToken);
                }
            }
        }

        private static void BuildFactories(ApplicationDbContext dbContext)
        {
            foreach (var factory in dbContext.Factories.Where(f => f.UpgradeFinishTime <= DateTime.Now))
            {
                var factoryStats = dbContext.FactoryStats.FirstOrDefault(f => f.FactoryType == factory.FactoryType && f.Level == factory.Level + 1);

                if(factoryStats == null)//max level? take care of that upon starting the upgrade
                {
                    return;
                }

                factory.Level += 1;
                factory.Income = factoryStats.Income;
                factory.UpgradeCost = factoryStats.UpgradeCost;
                factory.UpgradeTimeLength = factoryStats.UpgradeTimeLength;
                factory.BuildingSpace = factoryStats.BuildingSpace;
                factory.UpgradeFinishTime = null;
            }
        }

        private static void BuildDefences(ApplicationDbContext dbContext)
        {
            List<DefensiveStructure> defences = new List<DefensiveStructure>();
            foreach (var defenceBuilder in dbContext.DefenceBuilders.Where(d => d.FinishedBuildingTime <= DateTime.Now))
            {
                var defenceStats = dbContext.DefensiveStructureStats.First(d => d.Type == defenceBuilder.DefensiveStructureType);

                for (int i = 0; i < defenceBuilder.Count; i++)
                {
                    defences.Add(new DefensiveStructure(defenceBuilder.DefensiveStructureType, defenceBuilder.SystemId, defenceStats.MaxHP, defenceStats.Damage));
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
                var shipStats = dbContext.ShipStats.First(s => s.Type == shipBuilder.ShipType);

                for (int i = 0; i < shipBuilder.Count; i++)
                {
                    ships.Add(new Ship(shipBuilder.ShipType, shipBuilder.SystemId, playerId, shipStats.Damage, shipStats.MaxHP, shipStats.MaxStorage, shipStats.Speed, shipStats.FuelExpense));
                }

                shipBuilder.FinishedBuildingTime = null;
                shipBuilder.Count = 0;
            }

            dbContext.AddRange(ships);
        }
    }
}

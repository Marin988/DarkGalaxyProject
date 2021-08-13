using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Services.SystemServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DarkGalaxyProject.BackgroundTasks
{
    public class ResourceGrowing : BackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public ResourceGrowing(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var stopWatch = new Stopwatch();

                    stopWatch.Start();

                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var resources = dbContext.Resources;

                    foreach (var resource in resources)
                    {
                        var planetsId = dbContext.Planets.Where(p => p.SystemId == resource.SystemId).Select(p => p.Id).ToList();
                        var sun = dbContext.Suns.First(s => s.SystemId == resource.SystemId);

                        var totalIncome = 0;

                        var factoryType = factoryTypeDependingOnResource(resource);

                        foreach (var planetId in planetsId)
                        {
                            totalIncome += dbContext.Factories.First(f => f.PlanetId == planetId && f.FactoryType == factoryType).Income;
                        }

                        if (resource.Type == ResourceType.Energy)
                        {
                            totalIncome *= (int)sun.Type;
                        }

                        if (resource.Type == ResourceType.Fuel)
                        {
                            totalIncome /= 2;
                        }

                        resource.Quantity += totalIncome;
                    }

                    try
                    {
                        await dbContext.SaveChangesAsync();
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine($"Message: {error.Message}");
                        Console.WriteLine($"Inner exception: {error.InnerException}");
                        Console.WriteLine($"Source: ResourceGrowing");
                    }

                    int waitTime = 1000 - (int)stopWatch.ElapsedMilliseconds;

                    if (stopWatch.ElapsedMilliseconds > 1000)
                    {
                        waitTime = 100;
                    }

                    await Task.Delay(waitTime, stoppingToken);
                }
            }
        }

        private static FactoryType factoryTypeDependingOnResource(Resource resource)
        {
            FactoryType factoryType = 0;

            switch (resource.Type)
            {
                case ResourceType.MilkyCoin:
                    factoryType = FactoryType.Factory;
                    break;
                case ResourceType.Fuel:
                    factoryType = FactoryType.Factory;
                    break;
                case ResourceType.Paper:
                    factoryType = FactoryType.ResearchBuilding;
                    break;
                case ResourceType.Energy:
                    factoryType = FactoryType.SolarPanel;
                    break;
            }

            return factoryType;
        }
    }
}

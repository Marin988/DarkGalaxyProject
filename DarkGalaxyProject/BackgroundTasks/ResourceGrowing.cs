using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.WithinSystem;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
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
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var resources = dbContext.Resources;

                    //var systems = dbContext.Systems.Include(s => s.Resources).Include(s => s.Planets).ThenInclude(p => p.Factories).ToList();

                    foreach (var resource in resources)
                    {
                        var planetsId = dbContext.Planets.Where(p => p.SystemId == resource.SystemId).Select(p => p.Id).ToList();

                        var totalIncome = 0;

                        foreach (var planetId in planetsId)
                        {
                            totalIncome += dbContext.Factories.First(f => f.PlanetId == planetId).Income;
                        }

                        resource.Quantity += totalIncome;
                    }
                    dbContext.SaveChanges();

                    if (dbContext.ChangeTracker.HasChanges())
                    {
                        dbContext.ChangeTracker.Entries();
                        Console.WriteLine("YEP");
                    }

                    await Task.Delay(1000);
                }
            }
        }
    }
}

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
        //private readonly ApplicationDbContext data;
        //private readonly UserManager<Player> userManager;

        public ResourceGrowing(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    //var resources = data.Resources;

                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    var resources = dbContext.Resources;

                    var systems = dbContext.Systems.Include(s => s.Resources).Include(s => s.Planets).ThenInclude(p => p.Factories).ToList();

                    //foreach (var resource in resources)
                    //{
                    //    resource.Quantity += 1;
                    //}

                    foreach (var system in systems)
                    {
                        foreach (var resource in system.Resources)
                        {
                            resource.Quantity += system.Planets.Sum(p => p.Factories.Income);
                        }
                    }
                    //data.SaveChanges();
                    dbContext.SaveChanges();

                    await Task.Delay(1000);
                }
            }
        }
    }
}

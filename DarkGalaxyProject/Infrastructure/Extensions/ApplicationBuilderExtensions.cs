using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            var data = services.GetService<ApplicationDbContext>();

            var seeders = services.GetServices<IDatabaseSeeder>();

            foreach (var seeder in seeders)
            {
                seeder.Seed();
                //seeder.SeedUsers();
            }

            return app;
        }
    }
}

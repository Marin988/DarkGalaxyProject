using DarkGalaxyProject.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DarkGalaxyProject.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            //var data = services.GetService<ApplicationDbContext>();

            var seeders = services.GetServices<IDatabaseSeeder>();

            foreach (var seeder in seeders)
            {
                seeder.Seed();
            }

            return app;
        }
    }
}

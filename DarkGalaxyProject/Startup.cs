using DarkGalaxyProject.BackgroundTasks;
using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Infrastructure.Extensions;
using DarkGalaxyProject.Seeders;
using DarkGalaxyProject.Services.AllianceServices;
using DarkGalaxyProject.Services.AuctionServices;
using DarkGalaxyProject.Services.PartialServices;
using DarkGalaxyProject.Services.PlanetServices;
using DarkGalaxyProject.Services.PlayerServices;
using DarkGalaxyProject.Services.SystemServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<Player>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            services.AddHostedService<ResourceGrowing>();
            services.AddHostedService<FleetMovement>();
            services.AddHostedService<Upgrader>();

            services.AddMemoryCache();

            services.AddTransient<IDatabaseSeeder, EntityStatsSeeder>();
            services.AddTransient<IDatabaseSeeder, SystemsSeeder>();
            services.AddTransient<IDatabaseSeeder, PlayersSeeder>();
            services.AddTransient<IAuctionService, AuctionService>();
            services.AddTransient<IAllianceService, AllianceService>();
            services.AddTransient<IPlanetService, PlanetService>();
            services.AddTransient<IPlayerService, PlayerService>();
            services.AddTransient<ISystemService, SystemService>();
            services.AddTransient<IPartialService, PartialService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.Initialize();
        }
    }
}

using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.BaseModels;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using DarkGalaxyProject.Data.Models.WithinSystem.Buildings;
using DarkGalaxyProject.Data.Models.WithinSystem.Planets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DarkGalaxyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<Player> Players { get; set; }
        public DbSet<ResourcePlanet> ResourcePlanets { get; set; }
        public DbSet<EnergyPlanet> EnergyPlanets { get; set; }
        public DbSet<ResearchPlanet> ResearchPlanets { get; set; }
        public DbSet<PopulatedPlanet> PopulatedPlanets { get; set; }
        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<ResourceBuilding> ResourceBuildings { get; set; }
        public DbSet<StorageBuilding> StorageBuildings { get; set; }
        public DbSet<ResearchBuilding> ResearchBuildings { get; set; }
        public DbSet<DefensiveStructure> DefensiveStructures { get; set; }
        public DbSet<BlackHole> BlackHoles { get; set; }
        public DbSet<Sun> Suns { get; set; }
        public DbSet<Debris> Debrises { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResearchTree> ResearchTrees { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Models.System> Systems { get; set; }
        public DbSet<LivingQuarters> LivingQuarters { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=aspnet-DarkGalaxyProject-600B2675-F768-4905-B48D-802F1FBDE09D;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Alliance>()
                .HasOne(a => a.Leader)
                .WithOne(p => p.AllianceLeader)
                .HasForeignKey<Player>(p => p.AllianceLeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Alliance>()
                .HasMany(a => a.Leaders)
                .WithOne(p => p.AllianceLeaders)
                .HasForeignKey(p => p.AllianceLeadersId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Alliance>()
                .HasMany(a => a.Members)
                .WithOne(p => p.Alliance)
                .HasForeignKey(p => p.AllianceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Player>()
                .HasMany(p => p.Systems)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Models.System>()
                .HasOne(s => s.User)
                .WithMany(p => p.Systems)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(p => p.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<Message>()
               .HasOne(m => m.Sender)
               .WithMany(p => p.SentMessages)
               .HasForeignKey(m => m.SenderId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<EnergyPlanet>()
                .HasOne(e => e.System)
                .WithOne(s => s.EnergyPlanet)
                .HasForeignKey<Models.System>(s => s.EnergyPlanetId);

            builder
                .Entity<ResearchPlanet>()
                .HasOne(e => e.System)
                .WithOne(s => s.ResearchPlanet)
                .HasForeignKey<Models.System>(s => s.ResearchPlanetId);

            builder
                .Entity<ResourcePlanet>()
                .HasOne(e => e.System)
                .WithOne(s => s.ResourcePlanet)
                .HasForeignKey<Models.System>(s => s.ResourcePlanetId);

            builder
                .Entity<PopulatedPlanet>()
                .HasOne(e => e.System)
                .WithOne(s => s.PopulatedPlanet)
                .HasForeignKey<Models.System>(s => s.PopulatedPlanetId);

            builder
                .Entity<Alliance>()
                .HasMany(a => a.Messages)
                .WithOne(m => m.Alliance)
                .HasForeignKey(m => m.AllianceId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(builder);
        }
    }
}

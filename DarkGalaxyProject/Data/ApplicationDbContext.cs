using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using DarkGalaxyProject.Data.Models.WithinSystem;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DarkGalaxyProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<Player> 
    {

        public DbSet<Player> Players { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Alliance> Alliances { get; set; }
        public DbSet<DefensiveStructure> DefensiveStructures { get; set; }
        public DbSet<Sun> Suns { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResearchTree> ResearchTrees { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Models.System> Systems { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Factories> Factories { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=DarkGalaxy;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Factories>()
                .HasOne(f => f.Planet)
                .WithOne(p => p.Factories)
                .HasForeignKey<Factories>(f => f.PlanetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Alliance>()
                .HasOne(a => a.Leader)
                .WithOne(p => p.AllianceLeader)
                .HasForeignKey<Player>(p => p.AllianceLeaderId)
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
                .Entity<Planet>()
                .HasOne(e => e.System)
                .WithMany(s => s.Planets)
                .HasForeignKey(p => p.SystemId)
                .OnDelete(DeleteBehavior.Restrict);

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

﻿// <auto-generated />
using System;
using DarkGalaxyProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DarkGalaxyProject.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Alliance", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LeaderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Alliances");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.AuctionDeal", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BuyerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SellerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ShipType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("AuctionDeals");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Others.ChatMessage", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AllianceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AllianceId");

                    b.HasIndex("PlayerId");

                    b.ToTable("ChatMessages");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Others.Message", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(240)
                        .HasColumnType("nvarchar(240)");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("TimeOfSending")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Others.ResearchTree", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLearned")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ResearchType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("ResearchTrees");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AllianceCandidateId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AllianceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AllianceLeaderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentSystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("AllianceCandidateId");

                    b.HasIndex("AllianceId");

                    b.HasIndex("AllianceLeaderId")
                        .IsUnique()
                        .HasFilter("[AllianceLeaderId] IS NOT NULL");

                    b.HasIndex("CurrentSystemId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.System", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CurrentPlayerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PlayerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Systems");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.DefenceBuilder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("DefensiveStructureType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishedBuildingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("DefenceBuilders");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.DefensiveStructure", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("DefensiveStructures");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Factories", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("FactoryType")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("PlanetId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("UpgradeFinishTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PlanetId");

                    b.ToTable("Factories");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Fleet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DestinationSystemPoistion")
                        .HasColumnType("int");

                    b.Property<int>("MissionType")
                        .HasColumnType("int");

                    b.Property<bool>("Outgoing")
                        .HasColumnType("bit");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("Fleets");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Planet", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BuiltUpSpace")
                        .HasColumnType("int");

                    b.Property<bool>("IsTerraformed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("Planets");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Resource", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .IsConcurrencyToken()
                        .HasColumnType("int");

                    b.Property<string>("SystemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Ship", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DealId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FleetId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("HP")
                        .HasColumnType("int");

                    b.Property<bool>("OnMission")
                        .HasColumnType("bit");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Storage")
                        .HasColumnType("int");

                    b.Property<string>("SystemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DealId");

                    b.HasIndex("FleetId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("SystemId");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.ShipBuilder", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishedBuildingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShipType")
                        .HasColumnType("int");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("ShipBuilders");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Sun", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<string>("SystemId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SystemId");

                    b.ToTable("Suns");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.AuctionDeal", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Buyer")
                        .WithMany("BoughtAuctionDeals")
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Seller")
                        .WithMany("SoldAuctionDeals")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Others.ChatMessage", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Alliance", "Alliance")
                        .WithMany("Messages")
                        .HasForeignKey("AllianceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alliance");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Others.Message", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Others.ResearchTree", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Player")
                        .WithMany("ResearcheTree")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Player", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Alliance", "AllianceCandidate")
                        .WithMany("Candidates")
                        .HasForeignKey("AllianceCandidateId");

                    b.HasOne("DarkGalaxyProject.Data.Models.Alliance", "Alliance")
                        .WithMany("Members")
                        .HasForeignKey("AllianceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DarkGalaxyProject.Data.Models.Alliance", "AllianceLeader")
                        .WithOne("Leader")
                        .HasForeignKey("DarkGalaxyProject.Data.Models.Player", "AllianceLeaderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("DarkGalaxyProject.Data.Models.System", "CurrentSystem")
                        .WithOne("CurrentPlayer")
                        .HasForeignKey("DarkGalaxyProject.Data.Models.Player", "CurrentSystemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Alliance");

                    b.Navigation("AllianceCandidate");

                    b.Navigation("AllianceLeader");

                    b.Navigation("CurrentSystem");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.System", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Player")
                        .WithMany("Systems")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Player");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.DefenceBuilder", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", null)
                        .WithMany("DefenceBuildingQueue")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.DefensiveStructure", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", "System")
                        .WithMany("DefensiveStructures")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Factories", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.WithinSystem.Planet", "Planet")
                        .WithMany("Factories")
                        .HasForeignKey("PlanetId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Planet");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Fleet", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", null)
                        .WithMany("Fleets")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Planet", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", "System")
                        .WithMany("Planets")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Resource", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", null)
                        .WithMany("Resources")
                        .HasForeignKey("SystemId");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Ship", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.AuctionDeal", "Deal")
                        .WithMany("ShipsForSale")
                        .HasForeignKey("DealId");

                    b.HasOne("DarkGalaxyProject.Data.Models.WithinSystem.Fleet", null)
                        .WithMany("Ships")
                        .HasForeignKey("FleetId");

                    b.HasOne("DarkGalaxyProject.Data.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkGalaxyProject.Data.Models.System", "System")
                        .WithMany("Ships")
                        .HasForeignKey("SystemId");

                    b.Navigation("Deal");

                    b.Navigation("Player");

                    b.Navigation("System");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.ShipBuilder", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", null)
                        .WithMany("ShipBuildingQueue")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Sun", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.System", "System")
                        .WithMany("Suns")
                        .HasForeignKey("SystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("System");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DarkGalaxyProject.Data.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DarkGalaxyProject.Data.Models.Player", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Alliance", b =>
                {
                    b.Navigation("Candidates");

                    b.Navigation("Leader");

                    b.Navigation("Members");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.AuctionDeal", b =>
                {
                    b.Navigation("ShipsForSale");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.Player", b =>
                {
                    b.Navigation("BoughtAuctionDeals");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("ResearcheTree");

                    b.Navigation("SentMessages");

                    b.Navigation("SoldAuctionDeals");

                    b.Navigation("Systems");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.System", b =>
                {
                    b.Navigation("CurrentPlayer");

                    b.Navigation("DefenceBuildingQueue");

                    b.Navigation("DefensiveStructures");

                    b.Navigation("Fleets");

                    b.Navigation("Planets");

                    b.Navigation("Resources");

                    b.Navigation("ShipBuildingQueue");

                    b.Navigation("Ships");

                    b.Navigation("Suns");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Fleet", b =>
                {
                    b.Navigation("Ships");
                });

            modelBuilder.Entity("DarkGalaxyProject.Data.Models.WithinSystem.Planet", b =>
                {
                    b.Navigation("Factories");
                });
#pragma warning restore 612, 618
        }
    }
}

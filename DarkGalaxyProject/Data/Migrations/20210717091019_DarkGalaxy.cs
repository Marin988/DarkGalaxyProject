using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class DarkGalaxy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alliances",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LeaderId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alliances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Debrises",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timer = table.Column<int>(type: "int", nullable: false),
                    SizeType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debrises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchBuildings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchBuildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllianceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AllianceLeaderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AllianceLeadersId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AllianceCandidateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Alliances_AllianceCandidateId",
                        column: x => x.AllianceCandidateId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Alliances_AllianceLeaderId",
                        column: x => x.AllianceLeaderId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Alliances_AllianceLeadersId",
                        column: x => x.AllianceLeadersId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AllianceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessages_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatMessages_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReceiverId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Research",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResearchBuildingId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Research", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Research_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Research_ResearchBuildings_ResearchBuildingId",
                        column: x => x.ResearchBuildingId,
                        principalTable: "ResearchBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResearchTrees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discovery = table.Column<bool>(type: "bit", nullable: false),
                    Shields = table.Column<bool>(type: "bit", nullable: false),
                    ReadyForBattle = table.Column<bool>(type: "bit", nullable: false),
                    FuelOptimization = table.Column<bool>(type: "bit", nullable: false),
                    EnergyBarriers = table.Column<bool>(type: "bit", nullable: false),
                    SpaceCascades = table.Column<bool>(type: "bit", nullable: false),
                    BattleShips = table.Column<bool>(type: "bit", nullable: false),
                    SpaceMonster = table.Column<bool>(type: "bit", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchTrees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchTrees_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KnowledgeCost = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technologies_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DefensiveStructures",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AllianceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefensiveStructures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefensiveStructures_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    AllianceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BlackHoleId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DebrisId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resources_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resources_Debrises_DebrisId",
                        column: x => x.DebrisId,
                        principalTable: "Debrises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ships",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    HP = table.Column<int>(type: "int", nullable: false),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AllianceId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ships_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ships_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AllianceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PopulatedPlanetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResourcePlanetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResearchPlanetId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EnergyPlanetId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Systems_Alliances_AllianceId",
                        column: x => x.AllianceId,
                        principalTable: "Alliances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Systems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlackHoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Storage = table.Column<int>(type: "int", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackHoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlackHoles_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suns",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suns_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amenities",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LivingQuarters",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivingQuarters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourceBuildings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceBuildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageBuildings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    PlanetId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageBuildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planet",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SolarPanelId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GeothermalPlantId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FuelToEnergyCenterId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Population = table.Column<int>(type: "int", nullable: true),
                    ResearchBuildingId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CrystalMineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FuelGeneratorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TitaniumMineId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CrystalStorageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FuelStorageId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TitaniumStorageId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planet_ResearchBuildings_ResearchBuildingId",
                        column: x => x.ResearchBuildingId,
                        principalTable: "ResearchBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_ResourceBuildings_CrystalMineId",
                        column: x => x.CrystalMineId,
                        principalTable: "ResourceBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_ResourceBuildings_FuelGeneratorId",
                        column: x => x.FuelGeneratorId,
                        principalTable: "ResourceBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_ResourceBuildings_FuelToEnergyCenterId",
                        column: x => x.FuelToEnergyCenterId,
                        principalTable: "ResourceBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_ResourceBuildings_GeothermalPlantId",
                        column: x => x.GeothermalPlantId,
                        principalTable: "ResourceBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_ResourceBuildings_SolarPanelId",
                        column: x => x.SolarPanelId,
                        principalTable: "ResourceBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_ResourceBuildings_TitaniumMineId",
                        column: x => x.TitaniumMineId,
                        principalTable: "ResourceBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_StorageBuildings_CrystalStorageId",
                        column: x => x.CrystalStorageId,
                        principalTable: "StorageBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_StorageBuildings_FuelStorageId",
                        column: x => x.FuelStorageId,
                        principalTable: "StorageBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planet_StorageBuildings_TitaniumStorageId",
                        column: x => x.TitaniumStorageId,
                        principalTable: "StorageBuildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_PlanetId",
                table: "Amenities",
                column: "PlanetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AllianceCandidateId",
                table: "AspNetUsers",
                column: "AllianceCandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AllianceId",
                table: "AspNetUsers",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AllianceLeaderId",
                table: "AspNetUsers",
                column: "AllianceLeaderId",
                unique: true,
                filter: "[AllianceLeaderId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AllianceLeadersId",
                table: "AspNetUsers",
                column: "AllianceLeadersId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlackHoles_SystemId",
                table: "BlackHoles",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_AllianceId",
                table: "ChatMessages",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessages_PlayerId",
                table: "ChatMessages",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DefensiveStructures_AllianceId",
                table: "DefensiveStructures",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_DefensiveStructures_SystemId",
                table: "DefensiveStructures",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_LivingQuarters_PlanetId",
                table: "LivingQuarters",
                column: "PlanetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_CrystalMineId",
                table: "Planet",
                column: "CrystalMineId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_CrystalStorageId",
                table: "Planet",
                column: "CrystalStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_FuelGeneratorId",
                table: "Planet",
                column: "FuelGeneratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_FuelStorageId",
                table: "Planet",
                column: "FuelStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_FuelToEnergyCenterId",
                table: "Planet",
                column: "FuelToEnergyCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_GeothermalPlantId",
                table: "Planet",
                column: "GeothermalPlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_ResearchBuildingId",
                table: "Planet",
                column: "ResearchBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_SolarPanelId",
                table: "Planet",
                column: "SolarPanelId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_TitaniumMineId",
                table: "Planet",
                column: "TitaniumMineId");

            migrationBuilder.CreateIndex(
                name: "IX_Planet_TitaniumStorageId",
                table: "Planet",
                column: "TitaniumStorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Research_PlayerId",
                table: "Research",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Research_ResearchBuildingId",
                table: "Research",
                column: "ResearchBuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchTrees_PlayerId",
                table: "ResearchTrees",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceBuildings_PlanetId",
                table: "ResourceBuildings",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_AllianceId",
                table: "Resources",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_BlackHoleId",
                table: "Resources",
                column: "BlackHoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_DebrisId",
                table: "Resources",
                column: "DebrisId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_PlayerId",
                table: "Resources",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_SystemId",
                table: "Resources",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AllianceId",
                table: "Ships",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_PlayerId",
                table: "Ships",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ships_SystemId",
                table: "Ships",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageBuildings_PlanetId",
                table: "StorageBuildings",
                column: "PlanetId");

            migrationBuilder.CreateIndex(
                name: "IX_Suns_SystemId",
                table: "Suns",
                column: "SystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_AllianceId",
                table: "Systems",
                column: "AllianceId");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_EnergyPlanetId",
                table: "Systems",
                column: "EnergyPlanetId",
                unique: true,
                filter: "[EnergyPlanetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_PopulatedPlanetId",
                table: "Systems",
                column: "PopulatedPlanetId",
                unique: true,
                filter: "[PopulatedPlanetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ResearchPlanetId",
                table: "Systems",
                column: "ResearchPlanetId",
                unique: true,
                filter: "[ResearchPlanetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_ResourcePlanetId",
                table: "Systems",
                column: "ResourcePlanetId",
                unique: true,
                filter: "[ResourcePlanetId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_UserId",
                table: "Systems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_PlayerId",
                table: "Technologies",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_DefensiveStructures_Systems_SystemId",
                table: "DefensiveStructures",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_BlackHoles_BlackHoleId",
                table: "Resources",
                column: "BlackHoleId",
                principalTable: "BlackHoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Systems_SystemId",
                table: "Resources",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Systems_SystemId",
                table: "Ships",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_Planet_EnergyPlanetId",
                table: "Systems",
                column: "EnergyPlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_Planet_PopulatedPlanetId",
                table: "Systems",
                column: "PopulatedPlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_Planet_ResearchPlanetId",
                table: "Systems",
                column: "ResearchPlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_Planet_ResourcePlanetId",
                table: "Systems",
                column: "ResourcePlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Planet_PlanetId",
                table: "Amenities",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LivingQuarters_Planet_PlanetId",
                table: "LivingQuarters",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceBuildings_Planet_PlanetId",
                table: "ResourceBuildings",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageBuildings_Planet_PlanetId",
                table: "StorageBuildings",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceBuildings_Planet_PlanetId",
                table: "ResourceBuildings");

            migrationBuilder.DropForeignKey(
                name: "FK_StorageBuildings_Planet_PlanetId",
                table: "StorageBuildings");

            migrationBuilder.DropTable(
                name: "Amenities");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "DefensiveStructures");

            migrationBuilder.DropTable(
                name: "LivingQuarters");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Research");

            migrationBuilder.DropTable(
                name: "ResearchTrees");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropTable(
                name: "Ships");

            migrationBuilder.DropTable(
                name: "Suns");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BlackHoles");

            migrationBuilder.DropTable(
                name: "Debrises");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Alliances");

            migrationBuilder.DropTable(
                name: "Planet");

            migrationBuilder.DropTable(
                name: "ResearchBuildings");

            migrationBuilder.DropTable(
                name: "ResourceBuildings");

            migrationBuilder.DropTable(
                name: "StorageBuildings");
        }
    }
}

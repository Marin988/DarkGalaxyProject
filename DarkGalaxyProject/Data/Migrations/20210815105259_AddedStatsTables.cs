using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AddedStatsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHP",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxStorage",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildTime",
                table: "ShipBuilders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PricePerShip",
                table: "ShipBuilders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ResearchTrees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildingSpace",
                table: "Factories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Income",
                table: "Factories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpgradeCost",
                table: "Factories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpgradeTimeLength",
                table: "Factories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Damage",
                table: "DefensiveStructures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHP",
                table: "DefensiveStructures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BuildTime",
                table: "DefenceBuilders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PricePerUnit",
                table: "DefenceBuilders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DefensiveStructureStats",
                columns: table => new
                {
                    Type = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    BuildTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefensiveStructureStats", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "FactoryStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FactoryType = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Income = table.Column<int>(type: "int", nullable: false),
                    UpgradeCost = table.Column<int>(type: "int", nullable: false),
                    UpgradeTimeLength = table.Column<int>(type: "int", nullable: false),
                    BuildingSpace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FactoryStats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResearchTreeStats",
                columns: table => new
                {
                    ResearchType = table.Column<int>(type: "int", nullable: false),
                    IsLearned = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchTreeStats", x => x.ResearchType);
                });

            migrationBuilder.CreateTable(
                name: "ShipStats",
                columns: table => new
                {
                    Type = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    MaxHP = table.Column<int>(type: "int", nullable: false),
                    MaxStorage = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    BuildTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipStats", x => x.Type);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefensiveStructureStats");

            migrationBuilder.DropTable(
                name: "FactoryStats");

            migrationBuilder.DropTable(
                name: "ResearchTreeStats");

            migrationBuilder.DropTable(
                name: "ShipStats");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "MaxHP",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "MaxStorage",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "BuildTime",
                table: "ShipBuilders");

            migrationBuilder.DropColumn(
                name: "PricePerShip",
                table: "ShipBuilders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ResearchTrees");

            migrationBuilder.DropColumn(
                name: "BuildingSpace",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "UpgradeCost",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "UpgradeTimeLength",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "Damage",
                table: "DefensiveStructures");

            migrationBuilder.DropColumn(
                name: "MaxHP",
                table: "DefensiveStructures");

            migrationBuilder.DropColumn(
                name: "BuildTime",
                table: "DefenceBuilders");

            migrationBuilder.DropColumn(
                name: "PricePerUnit",
                table: "DefenceBuilders");
        }
    }
}

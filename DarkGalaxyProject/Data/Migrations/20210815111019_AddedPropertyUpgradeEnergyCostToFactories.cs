using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AddedPropertyUpgradeEnergyCostToFactories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UpgradeEnergyCost",
                table: "FactoryStats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpgradeEnergyCost",
                table: "Factories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpgradeEnergyCost",
                table: "FactoryStats");

            migrationBuilder.DropColumn(
                name: "UpgradeEnergyCost",
                table: "Factories");
        }
    }
}

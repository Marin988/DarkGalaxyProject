using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AddedResearchEnumsShipEnumsFactoryEnumsPlanetPropertyIsTerraformed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Factories_PlanetId",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "FactoriesId",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "UpgradeStartTime",
                table: "Factories");

            migrationBuilder.AddColumn<bool>(
                name: "IsTerraformed",
                table: "Planets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "FactoryType",
                table: "Factories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Factories_PlanetId",
                table: "Factories",
                column: "PlanetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Factories_PlanetId",
                table: "Factories");

            migrationBuilder.DropColumn(
                name: "IsTerraformed",
                table: "Planets");

            migrationBuilder.DropColumn(
                name: "FactoryType",
                table: "Factories");

            migrationBuilder.AddColumn<string>(
                name: "FactoriesId",
                table: "Planets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpgradeStartTime",
                table: "Factories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Factories_PlanetId",
                table: "Factories",
                column: "PlanetId",
                unique: true);
        }
    }
}

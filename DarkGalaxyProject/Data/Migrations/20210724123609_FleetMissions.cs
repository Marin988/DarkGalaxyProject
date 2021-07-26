using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class FleetMissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArrivalTime",
                table: "Systems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DepartureTime",
                table: "Systems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationSystemPoistion",
                table: "Systems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Outgoing",
                table: "Systems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OnMission",
                table: "Ships",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArrivalTime",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "DepartureTime",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "DestinationSystemPoistion",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "Outgoing",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "OnMission",
                table: "Ships");
        }
    }
}

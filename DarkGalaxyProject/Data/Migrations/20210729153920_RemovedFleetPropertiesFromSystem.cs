using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class RemovedFleetPropertiesFromSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flets_Systems_SystemId",
                table: "Flets");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Flets_FleetId",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Flets",
                table: "Flets");

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

            migrationBuilder.RenameTable(
                name: "Flets",
                newName: "Fleets");

            migrationBuilder.RenameIndex(
                name: "IX_Flets_SystemId",
                table: "Fleets",
                newName: "IX_Fleets_SystemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fleets",
                table: "Fleets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fleets_Systems_SystemId",
                table: "Fleets",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Fleets_FleetId",
                table: "Ships",
                column: "FleetId",
                principalTable: "Fleets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fleets_Systems_SystemId",
                table: "Fleets");

            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Fleets_FleetId",
                table: "Ships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fleets",
                table: "Fleets");

            migrationBuilder.RenameTable(
                name: "Fleets",
                newName: "Flets");

            migrationBuilder.RenameIndex(
                name: "IX_Fleets_SystemId",
                table: "Flets",
                newName: "IX_Flets_SystemId");

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
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Outgoing",
                table: "Systems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Flets",
                table: "Flets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Flets_Systems_SystemId",
                table: "Flets",
                column: "SystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Flets_FleetId",
                table: "Ships",
                column: "FleetId",
                principalTable: "Flets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

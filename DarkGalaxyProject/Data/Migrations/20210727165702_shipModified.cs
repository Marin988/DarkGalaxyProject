using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class shipModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Alliances_AllianceId",
                table: "Ships");

            migrationBuilder.DropIndex(
                name: "IX_Ships_AllianceId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "AllianceId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "FlyingDuration",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "SentOnMission",
                table: "Ships");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AllianceId",
                table: "Ships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FlyingDuration",
                table: "Ships",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "SentOnMission",
                table: "Ships",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AllianceId",
                table: "Ships",
                column: "AllianceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Alliances_AllianceId",
                table: "Ships",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

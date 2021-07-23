using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class UpgradeFinishTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpgradeTime",
                table: "Factories",
                newName: "UpgradeStartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpgradeFinishTime",
                table: "Factories",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpgradeFinishTime",
                table: "Factories");

            migrationBuilder.RenameColumn(
                name: "UpgradeStartTime",
                table: "Factories",
                newName: "UpgradeTime");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class Fleet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Systems_AspNetUsers_UserId",
                table: "Systems");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Systems",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Systems_UserId",
                table: "Systems",
                newName: "IX_Systems_PlayerId");

            migrationBuilder.AddColumn<string>(
                name: "FleetId",
                table: "Ships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Flets",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DestinationSystemPoistion = table.Column<int>(type: "int", nullable: true),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Outgoing = table.Column<bool>(type: "bit", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flets_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_FleetId",
                table: "Ships",
                column: "FleetId");

            migrationBuilder.CreateIndex(
                name: "IX_Flets_SystemId",
                table: "Flets",
                column: "SystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_Flets_FleetId",
                table: "Ships",
                column: "FleetId",
                principalTable: "Flets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_AspNetUsers_PlayerId",
                table: "Systems",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_Flets_FleetId",
                table: "Ships");

            migrationBuilder.DropForeignKey(
                name: "FK_Systems_AspNetUsers_PlayerId",
                table: "Systems");

            migrationBuilder.DropTable(
                name: "Flets");

            migrationBuilder.DropIndex(
                name: "IX_Ships_FleetId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "FleetId",
                table: "Ships");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                table: "Systems",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Systems_PlayerId",
                table: "Systems",
                newName: "IX_Systems_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_AspNetUsers_UserId",
                table: "Systems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

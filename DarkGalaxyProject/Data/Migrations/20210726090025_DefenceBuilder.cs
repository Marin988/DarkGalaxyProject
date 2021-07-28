using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class DefenceBuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DefenceBuilders",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SystemId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DefensiveStructureType = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    FinishedBuildingTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefenceBuilders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DefenceBuilders_Systems_SystemId",
                        column: x => x.SystemId,
                        principalTable: "Systems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DefenceBuilders_SystemId",
                table: "DefenceBuilders",
                column: "SystemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefenceBuilders");
        }
    }
}

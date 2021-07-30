using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class CurrentSystemToPlayerRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Systems_Alliances_AllianceId",
                table: "Systems");

            migrationBuilder.DropIndex(
                name: "IX_Systems_AllianceId",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "AllianceId",
                table: "Systems");

            migrationBuilder.AlterColumn<string>(
                name: "CurrentSystemId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentSystemId",
                table: "AspNetUsers",
                column: "CurrentSystemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Systems_CurrentSystemId",
                table: "AspNetUsers",
                column: "CurrentSystemId",
                principalTable: "Systems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Systems_CurrentSystemId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CurrentSystemId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AllianceId",
                table: "Systems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentSystemId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Systems_AllianceId",
                table: "Systems",
                column: "AllianceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Systems_Alliances_AllianceId",
                table: "Systems",
                column: "AllianceId",
                principalTable: "Alliances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

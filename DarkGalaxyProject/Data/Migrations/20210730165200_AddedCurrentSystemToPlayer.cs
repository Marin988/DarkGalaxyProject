using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AddedCurrentSystemToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_AspNetUsers_PlayerId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_PlayerId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Resources");

            migrationBuilder.AddColumn<string>(
                name: "CurrentPlayerId",
                table: "Systems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentSystemId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPlayerId",
                table: "Systems");

            migrationBuilder.DropColumn(
                name: "CurrentSystemId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "PlayerId",
                table: "Resources",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_PlayerId",
                table: "Resources",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_AspNetUsers_PlayerId",
                table: "Resources",
                column: "PlayerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AllianceDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Alliances",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Alliances");
        }
    }
}

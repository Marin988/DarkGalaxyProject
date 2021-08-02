using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class ResearchTreeFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Research");

            migrationBuilder.DropIndex(
                name: "IX_ResearchTrees_PlayerId",
                table: "ResearchTrees");

            migrationBuilder.DropColumn(
                name: "Goliath",
                table: "ResearchTrees");

            migrationBuilder.RenameColumn(
                name: "Vengeance",
                table: "ResearchTrees",
                newName: "IsLearned");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ResearchTrees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ResearchTrees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ResearchTrees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ResearchType",
                table: "ResearchTrees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ResearchTrees_PlayerId",
                table: "ResearchTrees",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ResearchTrees_PlayerId",
                table: "ResearchTrees");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ResearchTrees");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ResearchTrees");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ResearchTrees");

            migrationBuilder.DropColumn(
                name: "ResearchType",
                table: "ResearchTrees");

            migrationBuilder.RenameColumn(
                name: "IsLearned",
                table: "ResearchTrees",
                newName: "Vengeance");

            migrationBuilder.AddColumn<bool>(
                name: "Goliath",
                table: "ResearchTrees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Research",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Research", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Research_AspNetUsers_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ResearchTrees_PlayerId",
                table: "ResearchTrees",
                column: "PlayerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Research_PlayerId",
                table: "Research",
                column: "PlayerId");
        }
    }
}

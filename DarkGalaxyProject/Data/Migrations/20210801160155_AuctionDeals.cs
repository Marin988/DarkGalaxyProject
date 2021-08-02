using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AuctionDeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuctionDealId",
                table: "Ships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuctionDeals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SellerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDeals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionDeals_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionDeals_AspNetUsers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ships_AuctionDealId",
                table: "Ships",
                column: "AuctionDealId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDeals_BuyerId",
                table: "AuctionDeals",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionDeals_SellerId",
                table: "AuctionDeals",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_AuctionDeals_AuctionDealId",
                table: "Ships",
                column: "AuctionDealId",
                principalTable: "AuctionDeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_AuctionDeals_AuctionDealId",
                table: "Ships");

            migrationBuilder.DropTable(
                name: "AuctionDeals");

            migrationBuilder.DropIndex(
                name: "IX_Ships_AuctionDealId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "AuctionDealId",
                table: "Ships");
        }
    }
}

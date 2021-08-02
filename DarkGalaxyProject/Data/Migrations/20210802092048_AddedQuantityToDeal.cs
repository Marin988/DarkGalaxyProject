using Microsoft.EntityFrameworkCore.Migrations;

namespace DarkGalaxyProject.Data.Migrations
{
    public partial class AddedQuantityToDeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_AuctionDeals_AuctionDealId",
                table: "Ships");

            migrationBuilder.RenameColumn(
                name: "AuctionDealId",
                table: "Ships",
                newName: "DealId");

            migrationBuilder.RenameIndex(
                name: "IX_Ships_AuctionDealId",
                table: "Ships",
                newName: "IX_Ships_DealId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "AuctionDeals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ShipType",
                table: "AuctionDeals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_AuctionDeals_DealId",
                table: "Ships",
                column: "DealId",
                principalTable: "AuctionDeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ships_AuctionDeals_DealId",
                table: "Ships");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "AuctionDeals");

            migrationBuilder.DropColumn(
                name: "ShipType",
                table: "AuctionDeals");

            migrationBuilder.RenameColumn(
                name: "DealId",
                table: "Ships",
                newName: "AuctionDealId");

            migrationBuilder.RenameIndex(
                name: "IX_Ships_DealId",
                table: "Ships",
                newName: "IX_Ships_AuctionDealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ships_AuctionDeals_AuctionDealId",
                table: "Ships",
                column: "AuctionDealId",
                principalTable: "AuctionDeals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

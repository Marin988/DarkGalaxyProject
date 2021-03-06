namespace DarkGalaxyProject.Services.AuctionServices.Models
{
    public class DealServiceModel
    {
        public string Id { get; set; }

        public int Quantity { get; init; }

        public int Price { get; set; }

        public string SellerName { get; set; }

        public string BuyerId { get; set; }

        public string SellerId { get; set; }

        public string ShipType { get; set; }
    }
}

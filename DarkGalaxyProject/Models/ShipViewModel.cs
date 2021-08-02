namespace DarkGalaxyProject.Models
{
    public class ShipViewModel
    {
        public string Type { get; set; }

        public int Damage { get; set; }

        public int HP { get; set; }

        public int MaxHP { get; set; }

        public int MaxStorage { get; set; }

        public int Storage { get; set; }

        public int Speed { get; set; }

        public bool OnMission { get; set; }

        public string DealId { get; set; }
    }
}
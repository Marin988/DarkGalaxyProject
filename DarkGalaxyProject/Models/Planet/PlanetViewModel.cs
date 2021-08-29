namespace DarkGalaxyProject.Models.Planet
{
    public class PlanetViewModel
    {
        public string Id { get; set; }

        public string PlayerId { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public FactoriesViewModel Factories { get; set; }
    }
}

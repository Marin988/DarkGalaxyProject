using System.Collections.Generic;

namespace DarkGalaxyProject.Services.SystemServices.Models
{
    public class SystemServiceModel
    {
        public string Id { get; set; }

        public string PlayerId { get; set; }

        public string UserName { get; set; }

        public int Position { get; set; }

        public string Type { get; set; }

        public IEnumerable<PlanetListingServiceModel> Planets { get; set; }

        public IEnumerable<ShipServiceModel> Ships { get; set; }

        public IEnumerable<DefenceServiceModel> Defences { get; set; }
    }
}

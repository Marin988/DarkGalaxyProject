using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Fleet
    {
        public Fleet(string systemId)
        {
            Ships = new List<Ship>();
            SystemId = systemId;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int? DestinationSystemPoistion { get; set; }

        public DateTime? ArrivalTime { get; set; }

        public bool Outgoing { get; set; }

        public string SystemId { get; set; }

        public IEnumerable<Ship> Ships { get; set; }
    }
}

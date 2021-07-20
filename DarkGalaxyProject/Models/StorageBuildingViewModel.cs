using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class StorageBuildingViewModel
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public int ResourceCapacity { get; set; }

        public int EnergyCost { get; set; }

        public int Level { get; set; }
    }
}

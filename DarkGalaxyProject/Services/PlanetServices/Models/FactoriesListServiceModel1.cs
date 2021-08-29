using System;

namespace DarkGalaxyProject.Services.PlanetServices.Models
{
    public class FactorySeviceModel
    {
        public string Id { get; set; }

        public int Level { get; set; }

        public int Income { get; set; }

        public int UpgradeCost { get; set; }

        public DateTime? UpgradeFinishTime { get; set; }

        public int UpgradeTimeLength { get; set; }

        public string Type { get; set; }
    }
}

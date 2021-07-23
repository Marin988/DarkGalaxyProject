using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Planet
{
    public class FactoriesViewModel
    {
        public string Id { get; set; }

        public int Level { get; set; }

        public int Income { get; set; }

        public int UpgradeCost { get; set; }

        public DateTime? UpgradeFinishTime { get; set; }

        public DateTime? UpgradeStartTime { get; set; }

        public int UpgradeTimeLength { get; set; }
    }
}

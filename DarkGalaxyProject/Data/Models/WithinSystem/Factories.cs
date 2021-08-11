using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Factories
    {
        public Factories()
        {
            Level = 0;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Level { get; set; }

        public int Income => Level * 10 * (int)FactoryType;

        public int UpgradeCost => Level * 3000;

        public int UpgradeTimeLength => Level * 10;

        public int BuildingSpace => Level * 100;

        public FactoryType FactoryType { get; set; }

        public DateTime? UpgradeFinishTime { get; set; }

        [Required]
        public string PlanetId { get; set; }

        public Planet Planet { get; set; }
    }
}

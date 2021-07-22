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
            Level = 1;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public int Level { get; set; }

        public int Income => Level * 1000;

        public int UpgradeCost => Level * 3000;

        public int UpgradeTimeLength => Level * 10;

        public DateTime? UpgradeTime { get; set; }

        [Required]
        public string PlanetId { get; set; }

        public Planet Planet { get; set; }
    }
}

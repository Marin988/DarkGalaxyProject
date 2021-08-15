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
        public Factories(int income, int upgradeCost, int upgradeEnergyCost, int upgradeTimeLength, int buildingSpace, FactoryType factoryType, string planetId)
        {
            Level = 0;
            Income = income;
            UpgradeCost = upgradeCost;
            UpgradeEnergyCost = upgradeEnergyCost;
            UpgradeTimeLength = upgradeTimeLength;
            BuildingSpace = buildingSpace;
            FactoryType = factoryType;
            PlanetId = planetId;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int Level { get; set; }

        public int Income { get; set; }

        public int UpgradeCost { get; set; }

        public int UpgradeEnergyCost { get; set; }

        public int UpgradeTimeLength { get; set; }

        public int BuildingSpace { get; set; }

        [Required]
        public FactoryType FactoryType { get; set; }

        public DateTime? UpgradeFinishTime { get; set; }

        [Required]
        public string PlanetId { get; set; }

        public Planet Planet { get; set; }
    }
}

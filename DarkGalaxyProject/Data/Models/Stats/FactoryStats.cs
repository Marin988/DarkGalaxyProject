using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Stats
{
    public class FactoryStats
    {
        public FactoryStats(FactoryType factoryType, int level, int income, int upgradeCost, int upgradeEnergyCost, int upgradeTimeLength, int buildingSpace)
        {
            FactoryType = factoryType;
            Level = level;
            Income = income;
            UpgradeCost = upgradeCost;
            UpgradeEnergyCost = upgradeEnergyCost;
            UpgradeTimeLength = upgradeTimeLength;
            BuildingSpace = buildingSpace;
        }

        public int Id { get; init; }

        [Required]
        public FactoryType FactoryType { get; init; }

        public int Level { get; init; }

        public int Income { get; init; }

        public int UpgradeCost { get; init; }

        public int UpgradeEnergyCost { get; init; }

        public int UpgradeTimeLength { get; init; }

        public int BuildingSpace { get; init; }
    }
}

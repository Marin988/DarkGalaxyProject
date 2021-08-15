using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Stats
{
    public class ShipStats
    {
        public ShipStats(ShipType type, int damage, int maxHP, int maxStorage, int speed, int price, int buildTime, int fuelExpense)
        {
            Type = type;
            Damage = damage;
            MaxHP = maxHP;
            MaxStorage = maxStorage;
            Speed = speed;
            Price = price;
            BuildTime = buildTime;
            FuelExpense = fuelExpense;
        }

        [Key]
        [Required]
        public ShipType Type { get; init; }

        public int Damage { get; init; }

        public int MaxHP { get; init; }

        public int MaxStorage { get; init; }

        public int Speed { get; init; }

        public int Price { get; init; }

        public int FuelExpense { get; init; }

        public int BuildTime { get; init; }
    }
}

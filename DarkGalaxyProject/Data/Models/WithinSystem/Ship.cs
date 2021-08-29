using DarkGalaxyProject.Data.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class Ship
    {
        public Ship(ShipType type, string systemId, string playerId, int damage, int maxHP, int maxStorage, int speed, int fuelExpense)
        {
            Type = type;
            SystemId = systemId;
            HP = maxHP;
            PlayerId = playerId;
            Damage = damage;
            MaxHP = maxHP;
            MaxStorage = maxStorage;
            Speed = speed;
            FuelExpense = fuelExpense;
        }

        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        public ShipType Type { get; set; }

        public int Damage { get; init; }

        public int MaxHP { get; init; }

        public int HP { get; set; }

        public int MaxStorage { get; init; }

        public int Storage { get; set; }

        public int Speed { get; init; }

        public int FuelExpense { get; init; }

        public string FleetId { get; set; }

        public string SystemId { get; set; }

        public System System { get; set; }

        public bool OnMission { get; set; }

        [Required]
        public string PlayerId { get; set; }
        public Player Player { get; set; }

        public string DealId { get; set; }

        public AuctionDeal Deal { get; set; }
    }
}

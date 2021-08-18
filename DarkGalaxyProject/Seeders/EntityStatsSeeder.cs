using DarkGalaxyProject.Contracts;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using DarkGalaxyProject.Data.Models.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Seeders
{
    public class EntityStatsSeeder : IDatabaseSeeder
    {
        private readonly ApplicationDbContext data;

        public EntityStatsSeeder(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void Seed()
        {
            if(data.FactoryStats.Any())
            {
                return;
            }

            AddFactoryStats();
            AddResearchBuildingsStats();
            AddSolarPanelStats();
            AddResearchTreeStats();
            AddShipStats();
            AddDefenceStats();

            data.SaveChanges();
        }

        private void AddFactoryStats()
        {
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 0, 0, 200, 60, 10, 0));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 1, 20, 400, 100, 13, 100));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 2, 34, 900, 140, 30, 200));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 3, 52, 1600, 200, 62, 360));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 4, 80, 3000, 310, 97, 600));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 5, 112, 6500, 1420, 139, 800));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 6, 166, 9800, 3630, 221, 1000));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 7, 210, 13500, 7880, 480, 1300));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 8, 280, 20900, 11230, 590, 1750));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 9, 390, 34900, 21600, 800, 2000));
            data.FactoryStats.Add(new FactoryStats(FactoryType.Factory, 10, 600, 100000, 54000, 900, 2300));
        }

        private void AddResearchBuildingsStats()
        {
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 0, 0, 2000, 20, 10, 0));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 1, 5, 4000, 40, 75, 100));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 2, 15, 7000, 90, 90, 200));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 3, 33, 13000, 160, 115, 360));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 4, 46, 19000, 270, 160, 600));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 5, 61, 25000, 390, 240, 800));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 6, 78, 34000, 610, 300, 1000));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 7, 97, 41000, 860, 400, 1300));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 8, 113, 54000, 1100, 520, 1750));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 9, 140, 67000, 1280, 670, 2000));
            data.FactoryStats.Add(new FactoryStats(FactoryType.ResearchBuilding, 10, 280, 200000, 2500, 900, 2300));
        }

        private void AddSolarPanelStats()
        {
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 0, 0, 200, 60, 10, 0));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 1, 2, 600, 100, 13, 100));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 2, 4, 1400, 170, 30, 200));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 3, 7, 2100, 240, 62, 360));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 4, 10, 3400, 380, 97, 600));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 5, 14, 4900, 750, 139, 800));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 6, 20, 11100, 1750, 221, 1000));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 7, 23, 8300, 3090, 480, 1300));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 8, 28, 11000, 14390, 590, 1750));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 9, 37, 19000, 35900, 800, 2000));
            data.FactoryStats.Add(new FactoryStats(FactoryType.SolarPanel, 10, 49, 80000, 55000, 900, 2300));
        }

        private void AddResearchTreeStats()
        {
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.BattleShip, false, null, null, 100));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.Colonizing, false, null, null, 10000));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.Espionage, false, null, null, 50));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.FifthFleet, false, null, null, 20000));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.FourthFleet, false, null, null, 12000));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.HeavyDefence, false, null, null, 15000));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.SecondFleet, false, null, null, 500));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.Terraforming, false, null, null, 5000));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.ThirdFleet, false, null, null, 3000));
            data.ResearchTreeStats.Add(new ResearchTreeStats(ResearchType.TransportShip, false, null, null, 100));
        }

        public void AddShipStats()
        {
            data.ShipStats.Add(new ShipStats(ShipType.BattleShip, 40000, 40000, 2000, 250, 20000, 6, 100));
            data.ShipStats.Add(new ShipStats(ShipType.TransportShip, 0, 15000, 20000, 180, 20000, 12, 200));
            data.ShipStats.Add(new ShipStats(ShipType.Espionage, 0, 100, 0, 1000, 20000, 5, 50));
            data.ShipStats.Add(new ShipStats(ShipType.Colonizer, 0, 1000, 10000, 200, 20000, 60, 300));
        }

        public void AddDefenceStats()
        {
            data.DefensiveStructureStats.Add(new DefensiveStructureStats(DefensiveStructureType.Satelite, 4000, 4000, 3000, 5));
            data.DefensiveStructureStats.Add(new DefensiveStructureStats(DefensiveStructureType.SpaceStation, 1000000, 1000000, 80000, 300));
        }
    }
}

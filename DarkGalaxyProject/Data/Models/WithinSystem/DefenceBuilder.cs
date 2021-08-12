using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.WithinSystem
{
    public class DefenceBuilder
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string SystemId { get; set; }

        public DefensiveStructureType DefensiveStructureType { get; set; }

        public int Count { get; set; }

        public int Price => (int)DefensiveStructureType * 1000 * Count;

        public int BuildTime => (int)DefensiveStructureType * 5 * Count;

        public DateTime? FinishedBuildingTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.System
{
    public class BuildDefenceFormViewModel
    {
        public string SystemId { get; set; }

        public string DefenceType { get; set; }

        public int BuildTime { get; set; }

        public DateTime? FinishedBuildingTime { get; set; }

        public int Count { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices
{
    public class DefenceBuilderServiceModel
    {
        public string systemId { get; set; }

        public string DefenceType { get; set; }

        public int BuildTime { get; set; }

        public DateTime? FinishedBuildingTime { get; set; }

        public int Count { get; set; }
    }
}

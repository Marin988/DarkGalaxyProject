using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.SystemServices.Models
{
    public class DefenceBuilderServiceModel
    {
        public string systemId { get; set; }

        public string DefenceType { get; set; }

        public int BuildTime { get; set; }

        public int PricePerDefence { get; set; }

        public DateTime? FinishedBuildingTime { get; set; }

        [Range(1, 1000)]
        public int Count { get; set; }
    }
}

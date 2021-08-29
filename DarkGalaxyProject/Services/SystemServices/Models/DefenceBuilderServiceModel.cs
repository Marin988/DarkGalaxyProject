using System;
using System.ComponentModel.DataAnnotations;

namespace DarkGalaxyProject.Services.SystemServices.Models
{
    public class DefenceBuilderServiceModel
    {
        public string systemId { get; set; }

        public string DefenceType { get; set; }

        public int BuildTime { get; set; }

        public int PricePerUnit { get; set; }

        public int TotalPrice { get; set; }

        public DateTime? FinishedBuildingTime { get; set; }

        [Range(1, 1000)]
        public int Count { get; set; }
    }
}

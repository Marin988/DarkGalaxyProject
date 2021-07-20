using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models
{
    public class ResourceBuildingViewModel
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public int Production { get; set; }

        public int EnergyCost { get; set; }

        public int Level { get; set; }

        public string BuildingType => "ResourceBuilding";
    }
}

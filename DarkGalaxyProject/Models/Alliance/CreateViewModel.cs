using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class CreateViewModel
    {
        public string Name { get; set; }

        public AllianceType Type { get; set; }
    }
}

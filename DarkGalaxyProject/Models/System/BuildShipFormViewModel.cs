﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.System
{
    public class BuildShipFormViewModel
    {


        public string systemId { get; set; }

        public string ShipType { get; set; }

        public int BuildTime { get; set; }

        public DateTime? FinishedBuildingTime { get; set; }

        public int Count { get; set; }
    }
}
﻿using DarkGalaxyProject.Services.PlanetServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PlanetServices
{
    public interface IPlanetService
    {
        public PlanetServiceModel Planet(string planetId);

        public string StartUpgrade(string buildingId, string planetId, string playerId);

        public string Terraform(string planetId, string playerId);

    }
}

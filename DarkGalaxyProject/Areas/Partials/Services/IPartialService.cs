﻿using DarkGalaxyProject.Areas.Partials.Services.Models;
using DarkGalaxyProject.Services.SystemServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Areas.Partials.Services
{
    public interface IPartialService
    {
        public IEnumerable<SystemPartialServiceModel> PlayerSystems(string playerId);

        public IEnumerable<ResourceServiceModel> SystemResources(string playerId);

        public int PlayerUnseenMessagesCount(string playerId);
    }
}
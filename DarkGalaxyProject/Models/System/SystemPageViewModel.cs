using DarkGalaxyProject.Services.SystemServices;
using DarkGalaxyProject.Services.SystemServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.System
{
    public class SystemPageViewModel
    {
        public IEnumerable<SystemServiceModel> Systems { get; set; }

        public int Page { get; set; }

        public string CurrentSystemId { get; set; }
    }
}

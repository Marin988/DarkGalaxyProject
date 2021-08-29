using DarkGalaxyProject.Services.SystemServices.Models;
using System.Collections.Generic;

namespace DarkGalaxyProject.Models.System
{
    public class SystemPageViewModel
    {
        public IEnumerable<SystemServiceModel> Systems { get; set; }

        public int Page { get; set; }

        public string CurrentSystemId { get; set; }
    }
}

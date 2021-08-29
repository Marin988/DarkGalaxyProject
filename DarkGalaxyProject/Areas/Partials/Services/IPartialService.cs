using DarkGalaxyProject.Areas.Partials.Services.Models;
using System.Collections.Generic;

namespace DarkGalaxyProject.Areas.Partials.Services
{
    public interface IPartialService
    {
        public IEnumerable<SystemPartialServiceModel> PlayerSystems(string playerId);

        public IEnumerable<ResourceServiceModel> SystemResources(string playerId);

        public int PlayerUnseenMessagesCount(string playerId);
    }
}

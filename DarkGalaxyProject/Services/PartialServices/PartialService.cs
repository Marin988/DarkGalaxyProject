using DarkGalaxyProject.Data;
using DarkGalaxyProject.Services.PartialServices.Models;
using DarkGalaxyProject.Services.SystemServices;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.PartialServices
{
    public class PartialService : IPartialService
    {
        private readonly ApplicationDbContext data;

        public PartialService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<SystemPartialServiceModel> PlayerSystems(string playerId)
        {
            var playerSystems = data.Systems
                .Where(s => s.PlayerId == playerId)
                .Select(s => new SystemPartialServiceModel
                {
                    Id = s.Id,
                    Position = s.Position,
                    CurrentPlayerId = s.CurrentPlayerId
                })
                .ToList();

            return playerSystems;
        }

        public IEnumerable<ResourceServiceModel> SystemResources(string playerId)
        {
            var currentSystemId = data.Players.First(p => p.Id == playerId).CurrentSystemId;

            var resources = data.Resources
                .Where(r => r.SystemId == currentSystemId)
                .ToList()
                .Select(r => new ResourceServiceModel
                {
                    Quantity = r.Quantity,
                    Type = r.Type.ToString()
                })
                .OrderByDescending(r => r.Type)
                .ToList();

            return resources;
        }

        public int PlayerUnseenMessagesCount(string playerId)
        {
            var playerUnseenMessagesCount = data.Messages
                .Where(m => m.ReceiverId == playerId && !m.Seen)
                .Count();

            return playerUnseenMessagesCount;
        }
    }
}

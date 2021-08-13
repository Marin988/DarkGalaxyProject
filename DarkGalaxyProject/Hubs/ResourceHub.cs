using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DarkGalaxyProject.SharedLibrary;
using DarkGalaxyProject.Data;
using DarkGalaxyProject.Data.Enums;
using Microsoft.AspNetCore.Authorization;

namespace DarkGalaxyProject.Hubs
{
    [Authorize]
    public class ResourceHub : Hub
    {
        private readonly ApplicationDbContext data;

        public ResourceHub(ApplicationDbContext data)
        {
            this.data = data;
        }
        public async Task GetUpdatedResources(string systemId)
        {
            //CheckResult result;
            while (true)
            {
                var milkyCoin = data.Resources.First(r => r.SystemId == systemId && r.Type == ResourceType.MilkyCoin).Quantity.ToString();
                await Clients.Caller.SendAsync(milkyCoin);
                await Task.Delay(1000);
            }
        }
    }
}

using DarkGalaxyProject.Data.Models;
using DarkGalaxyProject.Data.Models.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Interfaces
{
    public interface ISpaceObject
    {
        public string Id { get; init; }

        public string Name { get; set; }

        public Coordinates Coord { get; init; }

        public int SystemId { get; set; }

    }
}

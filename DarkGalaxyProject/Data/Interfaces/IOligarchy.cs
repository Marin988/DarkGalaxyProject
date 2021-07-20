using DarkGalaxyProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Interfaces
{
    public interface IOligarchy
    {
        public IEnumerable<Player> Leaders { get; set; }
    }
}

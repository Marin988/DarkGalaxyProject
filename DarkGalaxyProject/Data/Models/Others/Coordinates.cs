using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    [Keyless]
    public class Coordinates
    {
        public int SystemPosition { get; set; }

        public int CurrentObjectPosition { get; set; }
    }
}

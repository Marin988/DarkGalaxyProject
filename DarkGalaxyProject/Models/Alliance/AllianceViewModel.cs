using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class AllianceViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Leader { get; set; }

        public List<string> Leaders { get; set; }

        public int MembersCount { get; set; }
    }
}

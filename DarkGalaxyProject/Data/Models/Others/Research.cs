using DarkGalaxyProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Models.Others
{
    public class Research
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public int Cost { get; set; }


        public string PlayerId { get; set; }
        public Player Player { get; set; }

    }
}

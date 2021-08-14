using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Services.AllianceServices.Models
{
    public class AllianceServiceModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Leader { get; set; }

        public string LeaderId { get; set; }

        [MinLength(6)]
        [MaxLength(120)]
        public string Description { get; set; }

        public int MembersCount { get; set; }
    }
}

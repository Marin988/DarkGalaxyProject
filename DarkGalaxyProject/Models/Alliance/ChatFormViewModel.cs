﻿using DarkGalaxyProject.Services.AllianceServices;
using DarkGalaxyProject.Services.AllianceServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Alliance
{
    public class ChatFormViewModel
    {
        public IEnumerable<ChatMessageServiceModel> Messages { get; set; }

        public string Player { get; set; }

        public string PlayerId { get; set; }

        public string AllianceId { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

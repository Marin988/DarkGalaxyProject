﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Models.Player
{
    public class MessageFormModel
    {
        public string SenderId { get; set; }

        [Required]
        public string ReceiverName { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

    }
}

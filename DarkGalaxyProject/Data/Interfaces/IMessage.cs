using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarkGalaxyProject.Data.Interfaces
{
    public interface IMessage
    {
        public string Content { get; set; }

        public DateTime? TimeOfSending { get; }
    }
}

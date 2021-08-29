using System;

namespace DarkGalaxyProject.Data.Interfaces
{
    public interface IMessage
    {
        public string Content { get; set; }

        public DateTime TimeOfSending { get; }
    }
}

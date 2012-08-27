using System;
using System.Linq;

namespace HeynsLibrary.Logging.Contracts
{
    public interface ILogger
    {
        string Name { get; set; }

        void Close();

        void Flush();

        void Write(string message, int id);

        void Write(Logging.EventType eventType, string message, int id);

        void Write(Logging.EventType eventType, int id, string format, params object[] args);
    }
}

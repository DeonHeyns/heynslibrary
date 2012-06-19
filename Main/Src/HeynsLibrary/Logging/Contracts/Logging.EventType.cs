using System;
using System.Linq;

namespace HeynsLibrary.Logging.Contracts
{
    public partial class Logging
    {
        public enum EventType : int
        {
            /// <summary>Fatal error or application crash.</summary>
            Critical = 1,
            /// <summary>Recoverable error.</summary>
            Error = 2,
            /// <summary>Noncritical problem.</summary>
            Warning = 4,
            /// <summary>Informational message.</summary>
            Information = 8,
            /// <summary>Debugging trace.</summary>
            Verbose = 16,
            /// <summary>Starting of a logical operation.</summary>
            Start = 256,
            /// <summary>Stopping of a logical operation.</summary>
            Stop = 512,
            /// <summary>Suspension of a logical operation.</summary>
            Suspend = 1024,
            /// <summary>Resumption of a logical operation.</summary>
            Resume = 2048,
            /// <summary>Changing of correlation identity.</summary>
            Transfer = 4096,
        }
    }
}
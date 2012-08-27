using System;
using System.Linq;
using HeynsLibrary.Logging.Contracts;

namespace HeynsLibrary.Logging
{
    public class TraceSource : ILogger
    {
        private readonly System.Diagnostics.TraceSource _traceSource;
        public string Name { get; set; }
        
        public TraceSource(string name = "TraceSourceLogger")
        {
            this.Name = name;
            this._traceSource = new System.Diagnostics.TraceSource(this.Name, System.Diagnostics.SourceLevels.All);
        }

        public void Close()
        {
            if (this._traceSource != null)
                this._traceSource.Close();
        }

        public void Flush()
        {
            if (this._traceSource != null)
                this._traceSource.Flush();
        }

        public void Write(string message, int id)
        {
            this._traceSource.TraceEvent(System.Diagnostics.TraceEventType.Information, id, message);
        }

        public void Write(Contracts.Logging.EventType eventType, string message, int id)
        {
            var @event = MapEventTypes(eventType);
            this._traceSource.TraceEvent(@event, id, message);
        }
  
        public void Write(Logging.Contracts.Logging.EventType eventType, int id, string format, params object[] args)
        {
            var @event = MapEventTypes(eventType);
            this._traceSource.TraceEvent(@event, id, format, args);
        }

        private System.Diagnostics.TraceEventType MapEventTypes(Logging.Contracts.Logging.EventType eventType)
        {
            System.Diagnostics.TraceEventType @event;
            switch (eventType)
            {
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Critical:
                    @event = System.Diagnostics.TraceEventType.Critical;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Error:
                    @event = System.Diagnostics.TraceEventType.Error;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Warning:
                    @event = System.Diagnostics.TraceEventType.Warning;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Information:
                    @event = System.Diagnostics.TraceEventType.Information;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Verbose:
                    @event = System.Diagnostics.TraceEventType.Verbose;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Start:
                    @event = System.Diagnostics.TraceEventType.Start;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Stop:
                    @event = System.Diagnostics.TraceEventType.Stop;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Suspend:
                    @event = System.Diagnostics.TraceEventType.Suspend;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Resume:
                    @event = System.Diagnostics.TraceEventType.Resume;
                    break;
                case HeynsLibrary.Logging.Contracts.Logging.EventType.Transfer:
                    @event = System.Diagnostics.TraceEventType.Transfer;
                    break;
                default:
                    @event = System.Diagnostics.TraceEventType.Information;
                    break;
            }
            return @event;
        }

    }
}

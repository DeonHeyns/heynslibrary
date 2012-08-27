using System;
using System.Linq;
using System.Threading;
using HeynsLibrary.Bootstrap.Contracts;
using HeynsLibrary.Logging.Contracts;

namespace HeynsLibrary.Bootstrap
{
    public class MapTypesBootstrapTask : IBootstrapTask
    {
        public Logging.Contracts.ILogger Logger { get; private set; }

        public MapTypesBootstrapTask(ILogger logger)
        {
            if (logger == null)
                throw new ArgumentNullException("logger");
            this.Logger = logger;
        }

        public void Execute()
        {
            if (Logger != null)
                Logger.Write(
                    string.Format("Start Execute of {0}", 
                    "HeynsLibrary.Bootstrap.MapTypesBootstrapTask"), 100);
            
            // Do work (simulation)
            Thread.Sleep(2000);

            if (Logger != null)
                Logger.Write(string.Format("End Execute of {0}", 
                    "HeynsLibrary.Bootstrap.MapTypesBootstrapTask"), 200);
        }
    }
}
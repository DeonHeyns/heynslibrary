using System;
using System.Linq;
using HeynsLibrary.Logging.Contracts;

namespace HeynsLibrary.Bootstrap.Contracts
{
    public interface IBootstrapTask
    {
        ILogger Logger { get; }

        void Execute();
    }
}

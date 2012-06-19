using System;
using System.Runtime.Serialization;

namespace HeynsLibrary.Exceptions
{
    [Serializable()]
    public class ServiceModelException : ManagedException
    {
        public ServiceModelException()
            : base() { }

        public ServiceModelException(string message)
            : base(message) { }

        public ServiceModelException(string message, Exception exception)
            : base(message, exception) { }

        protected ServiceModelException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
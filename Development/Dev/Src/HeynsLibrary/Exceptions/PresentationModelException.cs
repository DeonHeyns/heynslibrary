using System;
using System.Runtime.Serialization;

namespace HeynsLibrary.Exceptions
{
    [Serializable]
    public class PresentationModelException : ManagedException
    {
        public PresentationModelException()
            : base() { }

        public PresentationModelException(string message)
            : base(message) { }

        public PresentationModelException(string message, Exception exception)
            : base(message, exception) { }

        protected PresentationModelException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
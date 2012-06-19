using System;
using System.Runtime.Serialization;

namespace HeynsLibrary.Exceptions
{
    [Serializable()]
    public class ViewModelException : ManagedException
    {
        public ViewModelException()
            : base() { }

        public ViewModelException(string message)
            : base(message) { }

        public ViewModelException(string message, Exception exception)
            : base(message, exception) { }

        protected ViewModelException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
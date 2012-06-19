using System;
using System.Runtime.Serialization;

namespace HeynsLibrary.Exceptions
{
    [Serializable]
    public class DataModelException : ManagedException
    {
        public DataModelException()
            : base() { }

        public DataModelException(string message)
            : base(message) { }

        public DataModelException(string message, Exception exception)
            : base(message, exception) { }

        protected DataModelException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
using System;
using System.Runtime.Serialization;

namespace HeynsLibrary.Exceptions
{
    [Serializable]
    public class ManagedException : ApplicationException
    {
        /// <summary>
        /// Exception Severity:
        ///     Critical    - Failure Infrastructure
        ///     Error       - Failure Application
        ///     Warning     - Success Cautioned
        ///     Notice      - Success
        /// </summary>
        public enum SeverityLevel
        {
            Critical,
            Error,
            Warning,
            Notice
        }

        public SeverityLevel Severity { get; set; }

        public Guid Id { get; set; }

        public ManagedException()
            : base() { }

        public ManagedException(string message)
            : base(message) { }

        public ManagedException(string message, Exception exception)
            : base(message, exception) { }

        public ManagedException(Guid id, string message, Exception exception)
            : this(message, exception)
        {
            this.Id = id;
        }

        protected ManagedException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
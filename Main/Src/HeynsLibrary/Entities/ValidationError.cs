using System;

namespace HeynsLibrary.Entities
{
    [Serializable]
    public class ValidationError
    {
        public ValidationError()
        { }

        public ValidationError(string property, string message)
        {
            Property = property;
            Message = message;
        }

        public string Property { get; set; }

        public string Message { get; set; }
    }
}
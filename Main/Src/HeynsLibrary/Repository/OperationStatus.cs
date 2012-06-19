using System;
using System.Diagnostics;

namespace HeynsLibrary.Repository
{
    [DebuggerDisplay("Status: {Status}")]
    public class OperationStatus
    {
        public bool Status { get; set; }

        public int RecordsAffected { get; set; }

        public string Message { get; set; }

        public Object OperationID { get; set; }

        public string ExceptionMessage { get; set; }

        public static OperationStatus CreateFromException(string message, Exception ex)
        {
            var operationStatus = new OperationStatus
            {
                Status = false,
                Message = message,
                OperationID = null
            };

            if (ex != null)
            {
                operationStatus.ExceptionMessage = ex.Message;
            }

            return operationStatus;
        }
    }
}
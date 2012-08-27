namespace HeynsLibrary.Entities
{
    /// <summary>
    /// Service Status's
    /// </summary>
    /// <value>Success : Service Completed Successfully</value>
    /// <value>Failure : Service Failure - Critical</value>
    /// <value>Error   : Service Failure - Error</value>
    /// <value>Warning : Service Completed With Warnings</value>
    public enum ServiceStatus { Success, Failure, Error, Warning };

    /// <summary>
    /// Service Status Error Codes
    /// </summary>
    /// <value>None              :   No Error</value>
    /// <value>CriticalError     :   Infrastructure Error</value>
    /// <value>ValidationError   :   Validation Errors</value>
    /// <value>ApplicationError  :   Application Process Errors</value>
    public enum ServiceStatusError { None, CriticalError, ValidationError, ApplicationError };
}
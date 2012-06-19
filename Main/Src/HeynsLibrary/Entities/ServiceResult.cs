using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeynsLibrary.Entities
{
    [DataContract]
    public class ServiceResult
    {
        public ServiceResult()
        {
            StatusCode = ServiceStatus.Success;
            ErrorCode = ServiceStatusError.None;
            Message = string.Empty;
            Data = new Dictionary<string, string>();
        }

        [DataMember]
        public ServiceStatus StatusCode
        {
            get;
            set;
        }

        [DataMember]
        public ServiceStatusError ErrorCode
        {
            get;
            set;
        }

        [DataMember]
        public string Message
        {
            get;
            set;
        }

        [DataMember]
        public Dictionary<string, string> Data
        {
            get;
            set;
        }
    }

    [DataContract]
    public class ServiceResult<T> : ServiceResult
        where T : class
    {
        public T Result { get; set; }
    }
}
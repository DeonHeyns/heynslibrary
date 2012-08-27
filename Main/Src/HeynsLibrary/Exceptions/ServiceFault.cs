using System;
using System.Runtime.Serialization;
using HeynsLibrary.Entities;

namespace HeynsLibrary.Exceptions
{
    [DataContract(Name = "ServiceFault", Namespace = "http://ns.deonheyns.co.za/2011/01")]
    public class ServiceFault
    {
        public ServiceFault()
        {
            Status = ServiceStatus.Failure;
        }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ServiceStatus Status { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}
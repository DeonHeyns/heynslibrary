using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HeynsLibrary.Entities
{
    [DataContract(Namespace = "http://ns.deonheyns.co.za/2011/01")]
    public class BaseEntity
    {
        [DataMember]
        public List<ValidationError> ValidationErrors
        {
            get;
            set;
        }
    }
}
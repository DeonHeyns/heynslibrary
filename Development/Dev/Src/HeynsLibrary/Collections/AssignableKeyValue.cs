using System;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace HeynsLibrary.Collections
{
    [Serializable]
    [DataContract(Name="AssignableKeyValue")]
    [XmlRoot(ElementName = "AssignableKeyValue")]
    public class AssignableKeyValue<K, V>
    {
        [DataMember(Name="Key")]
        [XmlAttribute(AttributeName = "Key")]
        public K Key { get; set; }

        [DataMember(Name = "Value")]
        [XmlAttribute(AttributeName = "Value")]
        public V Value { get; set; }
    }
}

using System.Xml.Serialization;

namespace HeynsLibrary.Exceptions
{
    public class ServiceModelFault
    {
        [XmlAttribute]
        public string ErrorSymbol { get; set; }

        [XmlAttribute]
        public string ErrorCode { get; set; }

        [XmlAttribute]
        public string ErrorMessage { get; set; }
    }
}
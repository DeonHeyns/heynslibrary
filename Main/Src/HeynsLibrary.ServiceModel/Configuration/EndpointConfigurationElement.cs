using System;
using System.ComponentModel;
using System.Configuration;

namespace HeynsLibrary.ServiceModel.Configuration
{
    public class EndpointConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("type", IsRequired = false)]
        [TypeConverter(typeof(TypeNameConverter))]
        public Type type
        {
            get
            {
                return (Type)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }
    }
}
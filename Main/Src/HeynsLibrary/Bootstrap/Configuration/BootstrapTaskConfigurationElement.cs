using System;
using System.Linq;
using System.Configuration;
using System.ComponentModel;

namespace HeynsLibrary.Bootstrap.Configuration
{
    public class BootstrapTaskConfigurationElement : ConfigurationElement
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

        [ConfigurationProperty("type", IsRequired = true)]
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

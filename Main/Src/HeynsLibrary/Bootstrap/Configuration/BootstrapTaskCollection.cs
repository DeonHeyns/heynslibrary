using System;
using System.Linq;
using System.Configuration;

namespace HeynsLibrary.Bootstrap.Configuration
{
    public class BootstrapTaskCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new BootstrapTaskConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((BootstrapTaskConfigurationElement)element).Name;
        }

        public BootstrapTaskConfigurationElement this[int index]
        {
            get
            {
                return (BootstrapTaskConfigurationElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public BootstrapTaskConfigurationElement this[string name]
        {
            get
            {
                return (BootstrapTaskConfigurationElement)BaseGet(name);
            }           
        }
    }
}
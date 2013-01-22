using System.Configuration;

namespace HeynsLibrary.ServiceModel.Configuration
{
    public class EndpointCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EndpointConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EndpointConfigurationElement)element).Name;
        }

        public EndpointConfigurationElement this[int index]
        {
            get
            {
                return (EndpointConfigurationElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);
                BaseAdd(index, value);
            }
        }

        new public EndpointConfigurationElement this[string name]
        {
            get
            {
                return (EndpointConfigurationElement)BaseGet(name);
            }
        }
    }
}

using System;
using System.Configuration;

namespace HeynsLibrary.Configuration
{
    /// <summary>
    /// Collection Of Portal Configuration Sections
    /// </summary>
    public class PortalConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PortalConfiguration();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PortalConfiguration)element).Guid;
        }

        new public PortalConfiguration this[string Name]
        {
            get
            {
                foreach (PortalConfiguration site in this)
                {
                    if (site.Name == Name)
                    {
                        return site;
                    }
                }
                return new PortalConfiguration();
            }
        }

        public PortalConfiguration this[Guid Id]
        {
            get
            {
                foreach (PortalConfiguration site in this)
                {
                    if (site.Guid == Id)
                        return site;
                }
                return new PortalConfiguration();
            }
        }
    }
}

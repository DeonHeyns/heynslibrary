using System.Configuration;

namespace HeynsLibrary.Configuration
{
    public class PortalMetaConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("Portals", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(PortalConfigurationCollection), AddItemName = "Portal")]
        public PortalConfigurationCollection Portals
        {
            get { return (PortalConfigurationCollection)base["Portals"]; }
        }
    }
}

using System;
using System.Configuration;

namespace HeynsLibrary.Configuration
{
    /// <summary>
    /// Individual Portal Site Configuration Section
    /// </summary>
    public class PortalConfiguration : ConfigurationSection
    {
        /// <summary>
        /// ConfigurationElement Attribute ID
        /// </summary>
        [ConfigurationProperty("guid", IsRequired = true, IsKey = true)]
        public Guid Guid
        {
            get { return (Guid)this["guid"]; }
        }

        /// <summary>
        /// ConfigurationElement Attribute Name
        /// </summary>
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
        }

        /// <summary>
        /// ConfigurationElement Attribute Host
        /// </summary>
        [ConfigurationProperty("host", IsRequired = true)]
        public string Host
        {
            get { return (string)this["host"]; }
        }

        /// <summary>
        /// ConfigurationElement Attribute Theme
        /// </summary>
        [ConfigurationProperty("theme", IsRequired = true)]
        public string Theme
        {
            get { return (string)this["theme"]; }
        }

        /// <summary>
        /// ConfigurationElement Attribute Sitemap
        /// </summary>
        [ConfigurationProperty("sitemap", IsRequired = false)]
        public string Sitemap
        {
            get { return (string)this["sitemap"]; }
        }
    }
}
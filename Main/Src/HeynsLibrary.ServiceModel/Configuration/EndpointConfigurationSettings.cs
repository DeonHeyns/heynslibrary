using System.Configuration;

namespace HeynsLibrary.ServiceModel.Configuration
{
    public class EndpointConfigurationSettings : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = false,
            DefaultValue = "endpointConfigurationSettings")]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("endpoints", IsRequired = false)]
        public EndpointCollection Endpoints
        {
            get
            {
                var endpointCollection =
                    (EndpointCollection)base["endpoints"];
                return endpointCollection;
            }
        }
    }
}

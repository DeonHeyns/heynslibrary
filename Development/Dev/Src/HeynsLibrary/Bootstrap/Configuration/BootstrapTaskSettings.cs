using System;
using System.Linq;
using System.Configuration;

namespace HeynsLibrary.Bootstrap.Configuration
{
    public class BootstrapTaskSettings : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = true, 
            DefaultValue = "bootstrapTaskSettings")]
        public string Name 
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("tasks", IsRequired = false)]
        public BootstrapTaskCollection BootstrapTasks
        {
            get
            {
                BootstrapTaskCollection bootstrapTaskCollection = 
                    (BootstrapTaskCollection)base["tasks"];
                return bootstrapTaskCollection;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using HeynsLibrary.Bootstrap;
using HeynsLibrary.Bootstrap.Configuration;
using HeynsLibrary.Bootstrap.Contracts;
using HeynsLibrary.Logging;
using HeynsLibrary.Logging.Contracts;

namespace HeynsLibrary.ServiceModel
{
    /// <summary>
    /// Used by IoC Containers to inject dependencies
    /// </summary>
    public class BootstrapServiceHost : ServiceHost
    {
        private ILogger _logger;
        private List<IBootstrapTask> _bootStrapTasks { get; set; }
        public List<IBootstrapTask> BootStrapTasks
        {
            get { return this._bootStrapTasks ?? (this._bootStrapTasks = new List<IBootstrapTask>()); }
            set
            {
                this._bootStrapTasks = value;
            }
        }

        public ILogger Logger
        {
            get { return _logger ?? (_logger = new TraceSource("BootStrapServiceHost Logger")); }
        }

        protected override void InitializeRuntime()
        {
            // 0 = starting pipeline
            Logger.Write("InitializeRuntime() called", 0);
            // Typically you want to add this to the config and read all the bootstrap tasks from there...
            var bootTask = new MapTypesBootstrapTask(this.Logger);
            BootStrapTasks.Add(bootTask);
            // Get the rest of the tasks from the config file
            // Here you should use a IoC container to rather get your types. However if they
            // Use simple initialization like the Bootstrap tasks then this might be a good enough solution
            GetTasksFromConfig();
            ExecuteTasks();
            base.InitializeRuntime();
        }

        private void ExecuteTasks()
        {
            if (this.BootStrapTasks.Count <= 0) return;
            foreach (var task in this.BootStrapTasks)
            {
                this.Logger.Write(Logging.Contracts.Logging.EventType.Information, 50, 
                                  "Task: {0} Begin Execute at: {1}", task.GetType().FullName, DateTime.Now);
                task.Execute();
                this.Logger.Write(Logging.Contracts.Logging.EventType.Information, 60, 
                                  "Task: {0} Ended Execute at: {1}", task.GetType().FullName, DateTime.Now);
            }
        }

        private void GetTasksFromConfig()
        {
            var bootstrapTaskSettings = ConfigurationManager.GetSection("bootstrapTaskSettings") as BootstrapTaskSettings;
            if (bootstrapTaskSettings == null)
                return;
            var tasks = bootstrapTaskSettings.BootstrapTasks.OfType<BootstrapTaskConfigurationElement>().ToList();
            foreach (var task in tasks.Where(task => task.type.HasInterface<IBootstrapTask>()))
            {
                this.BootStrapTasks.Add(task.type.GenerateInstance<IBootstrapTask>());
            }
        }
    }
}
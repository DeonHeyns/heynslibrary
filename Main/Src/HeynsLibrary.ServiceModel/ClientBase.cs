using System;
using System.Configuration;
using System.Linq;
using HeynsLibrary.ServiceModel.Configuration;

namespace HeynsLibrary.ServiceModel
{
    public class ClientBase<T> : IDisposable
    where T : class
    {
        public T Channel { get; private set; }

        /// <summary>
        /// Allows users of this ClientBase to use the custom configuration found in <see cref="HeynsLibrary.ServiceModel.Configuration.EndpointConfigurationSettings"/>
        /// or to use the default System.ServiceModel configuration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public ClientBase()
        {
            Exception exception = null;

            try
            {
                CreateChannelFromServiceModelConfiguration();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            try
            {
                CreateChannelFromEndPointConfiguration();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, exception);
            }
        }

        private void CreateChannelFromServiceModelConfiguration()
        {
            var clientBase = new ServiceModelClientBase();
            Channel = clientBase.Channel;
        }

        private void CreateChannelFromEndPointConfiguration()
        {
            var endpointConfigurationSettings =
                ConfigurationManager.GetSection("endpointConfigurationSettings") as EndpointConfigurationSettings;
            if (endpointConfigurationSettings == null)
                throw new Exception("There is no section 'endpointConfigurationSettings' defined in the configuration file.");

            var endpoints = endpointConfigurationSettings.Endpoints.OfType<EndpointConfigurationElement>();
            var endpoint = endpoints.Single(e => e.type.HasInterface<T>());
            Channel = endpoint.type.GenerateInstance<T>();
        }

        private sealed class ServiceModelClientBase : System.ServiceModel.ClientBase<T>
        {
            internal new T Channel { get; private set; }

            internal ServiceModelClientBase()
            {
                this.Channel = base.Channel;
            }

            internal void Dispose()
            {
                try
                {
                    if (this.State == System.ServiceModel.CommunicationState.Faulted)
                        this.Abort();
                    else
                        this.Close();
                }
                catch
                {
                    this.Abort();
                }
            }
        }

        public void Dispose()
        {
            if (Channel is ServiceModelClientBase)
            {
                (Channel as ServiceModelClientBase).Dispose();
            }
        }
    }
}
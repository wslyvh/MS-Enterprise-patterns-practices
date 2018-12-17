using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;
using wslyvh.Core.Service.Host.Unity;

namespace wslyvh.Core.Service.Behaviors
{
    public class UnityServiceBehavior : IServiceBehavior
    {
        private readonly IUnityContainer _container;

        public UnityServiceBehavior(IUnityContainer container)
        {
            Guard.ArgumentIsNotNull(container, "container");

            _container = container;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                var cd = channelDispatcher as ChannelDispatcher;
                if (cd == null) 
                    continue;

                foreach (var endpointDispatcher in cd.Endpoints)
                {
                    if (endpointDispatcher.ContractName == "IMetadataExchange")
                        continue;

                    var serviceEndpoint = serviceDescription.Endpoints.FirstOrDefault(e => e.Contract.Name == endpointDispatcher.ContractName);
                    endpointDispatcher.DispatchRuntime.InstanceProvider = new UnityInstanceProvider(_container, serviceEndpoint.Contract.ContractType);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }
    }
}

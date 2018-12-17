using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using wslyvh.Core.Service.Host.Unity;

namespace wslyvh.Core.Service.Behaviors
{
    public class UnityContractBehavior : IContractBehavior
    {
        private readonly IInstanceProvider _instanceProvider;

        public UnityContractBehavior(IInstanceProvider instanceProvider)
        {
            Guard.ArgumentIsNotNull(instanceProvider, "instanceProvider");

            _instanceProvider = instanceProvider;
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            dispatchRuntime.InstanceProvider = _instanceProvider;
            dispatchRuntime.InstanceContextInitializers.Add(new UnityInstanceContextInitializer());
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
    }
}

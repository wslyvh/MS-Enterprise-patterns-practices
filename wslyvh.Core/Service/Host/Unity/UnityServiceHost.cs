using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Practices.Unity;
using wslyvh.Core.Service.Behaviors;

namespace wslyvh.Core.Service.Host.Unity
{
    public class UnityServiceHost : ServiceHost
    {
        public UnityServiceHost(IUnityContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            Guard.ArgumentIsNotNull(container, "container");

            ApplyServiceBehaviors(container);
            ApplyContractBehaviors(container);

            foreach (var contractDescription in ImplementedContracts.Values)
                contractDescription.Behaviors.Add(new UnityContractBehavior(new UnityInstanceProvider(container, contractDescription.ContractType)));
        }

        private void ApplyContractBehaviors(IUnityContainer container)
        {
            var registeredContractBehaviors = container.ResolveAll<IContractBehavior>();

            foreach (var contractBehavior in registeredContractBehaviors)
            {
                foreach (var contractDescription in ImplementedContracts.Values) 
                    contractDescription.Behaviors.Add(contractBehavior);
            }
        }

        private void ApplyServiceBehaviors(IUnityContainer container)
        {
            var registeredServiceBehaviors = container.ResolveAll<IServiceBehavior>();

            foreach (var serviceBehavior in registeredServiceBehaviors)
                Description.Behaviors.Add(serviceBehavior);
        }
    }
}

using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Service.Host.Unity
{
    public class UnityDependencyServiceHostFactory : UnityServiceHostFactory
    {
        [Dependency]
        public override IUnityContainer Container { get; set; }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new UnityServiceHost(Container, serviceType, baseAddresses);

            return host;
        }
    }
}

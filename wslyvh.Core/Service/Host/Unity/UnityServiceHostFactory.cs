using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Service.Host.Unity
{
    public abstract class UnityServiceHostFactory : ServiceHostFactory
    {
        public abstract IUnityContainer Container { get; set; }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new UnityServiceHost(Container, serviceType, baseAddresses);

            return host;
        }
    }
}

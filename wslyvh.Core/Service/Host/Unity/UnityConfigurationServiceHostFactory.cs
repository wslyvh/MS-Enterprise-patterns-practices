using System;
using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Service.Host.Unity
{
    public abstract class UnityConfigurationServiceHostFactory : UnityServiceHostFactory
    {
        private IUnityContainer _container;

        public override IUnityContainer Container
        {
            get
            {
                if (_container == null)
                    _container = ConfigureContainer();

                return _container;
            }
            set { _container = value; }
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new UnityServiceHost(_container, serviceType, baseAddresses);

            return host;
        }

        protected abstract IUnityContainer ConfigureContainer();
    }
}

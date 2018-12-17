using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Service.Host.Unity
{
    public class UnityInstanceProvider : IInstanceProvider
    {
        private readonly IUnityContainer _container;
        private readonly Type _contractType;

        public UnityInstanceProvider(IUnityContainer container, Type contractType)
        {
            Guard.ArgumentIsNotNull(container, "container");
            Guard.ArgumentIsNotNull(contractType, "contractType");

            _container = container;
            _contractType = contractType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return GetInstance(instanceContext, null);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            var childContainer = instanceContext.Extensions.Find<UnityInstanceContextExtension>().GetChildContainer(_container);

            return childContainer.Resolve(_contractType);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            instanceContext.Extensions.Find<UnityInstanceContextExtension>().DisposeChildContainer();
        }
    }
}

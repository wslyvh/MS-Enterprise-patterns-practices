using System.ServiceModel;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Service.Host.Unity
{
    public class UnityInstanceContextExtension : IExtension<InstanceContext>
    {
        private IUnityContainer _childContainer;

        public IUnityContainer GetChildContainer(IUnityContainer container)
        {
            Guard.ArgumentIsNotNull(container, "container");

            return _childContainer ?? (_childContainer = container.CreateChildContainer());
        }

        public void DisposeChildContainer()
        {
            if (_childContainer != null)
                _childContainer.Dispose();
        }

        public void Attach(InstanceContext owner)
        {
        }

        public void Detach(InstanceContext owner)
        {
        }
    }
}

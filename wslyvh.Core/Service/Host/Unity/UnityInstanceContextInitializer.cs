using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace wslyvh.Core.Service.Host.Unity
{
    public class UnityInstanceContextInitializer : IInstanceContextInitializer
    {
        public void Initialize(InstanceContext instanceContext, Message message)
        {
            instanceContext.Extensions.Add(new UnityInstanceContextExtension());
        }
    }
}

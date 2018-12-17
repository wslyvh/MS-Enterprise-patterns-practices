using System.ServiceModel;

namespace wslyvh.Core.Service
{
    [ServiceContract(Namespace = Namespaces.ServiceV1, Name = "IService")]
    public interface IService
    {
        bool IsAlive
        {
            [OperationContract]
            get;
        }
    }
}

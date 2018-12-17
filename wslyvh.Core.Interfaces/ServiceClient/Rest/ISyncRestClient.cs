using wslyvh.Core.Interfaces.ServiceClient.Enumerations;

namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface ISyncRestClient : IRestClient
    {
        IRestResponse Execute(Method method, string resource);
        IRestResponse<T> Execute<T>(Method method, string resource) where T : class;

        IRestResponse Execute(IRestRequest request);
        IRestResponse<T> Execute<T>(IRestRequest request) where T : class;
    }
}

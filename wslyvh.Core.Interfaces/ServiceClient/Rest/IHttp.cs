
namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface IHttp
    {
        IHttpResponse GetResponse(IRestRequest restRequest);
    }
}

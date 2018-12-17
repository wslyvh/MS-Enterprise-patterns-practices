
namespace wslyvh.Core.Samples.ServiceClient
{
    public interface IApiObjectFactory
    {
        T Create<T>(string id) where T : class;
    }
}

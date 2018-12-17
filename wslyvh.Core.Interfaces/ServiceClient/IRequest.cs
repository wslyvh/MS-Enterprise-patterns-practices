
namespace wslyvh.Core.Interfaces.ServiceClient
{
    public interface IRequest<T> where T : class
    {
        T Data { get; set; }
    }
}
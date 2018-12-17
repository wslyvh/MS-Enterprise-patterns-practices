
// ReSharper disable CheckNamespace
namespace wslyvh.Core.Interfaces.ServiceClient
// ReSharper restore CheckNamespace
{
    public class Request<T> : IRequest<T> where T : class
    {
        public T Data { get; set; }
    }
}

// ReSharper disable CheckNamespace
namespace wslyvh.Core.Interfaces.ServiceClient
// ReSharper restore CheckNamespace
{
    public class Response<T> : Response, IResponse<T>
    {
        public T Data { get; set; }
    }

    public class Response : IResponse
    {
        public IStatus Status { get; set; }

        public Response()
        {
            Status = new Status();
        }

        public Response(IStatus status)
        {
            Status = status;
        }
    }
}

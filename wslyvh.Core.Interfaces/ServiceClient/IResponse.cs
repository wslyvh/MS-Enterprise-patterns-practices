
namespace wslyvh.Core.Interfaces.ServiceClient
{
    public interface IResponse<T> : IResponse
    {
        T Data { get; set; }
    }

    public interface IResponse
    {
        IStatus Status { get; set; }
    }
}

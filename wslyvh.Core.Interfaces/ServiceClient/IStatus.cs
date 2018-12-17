
namespace wslyvh.Core.Interfaces.ServiceClient
{
    public interface IStatus
    {
        int Code { get; set; }
        string Message { get; set; }
        bool Success { get; set; }
    }
}

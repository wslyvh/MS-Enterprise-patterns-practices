
// ReSharper disable CheckNamespace
namespace wslyvh.Core.Interfaces.ServiceClient
// ReSharper restore CheckNamespace
{
    public class Status : IStatus
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public Status()
        {
            Success = false;
        }
    }
}

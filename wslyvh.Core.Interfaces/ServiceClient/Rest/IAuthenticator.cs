namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface IAuthenticator
    {
        void Authenticate(IRestRequest request);
    }
}

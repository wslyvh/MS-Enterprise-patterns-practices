using System.Net;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Authenticators
{
    public class NtlmAuthenticator : IAuthenticator
    {
        private readonly ICredentials _credentials;

        public NtlmAuthenticator(ICredentials credentials)
        {
            Guard.ArgumentIsNotNull<ICredentials>(credentials, "credentials");

            _credentials = credentials;
        }

        public void Authenticate(IRestRequest request)
        {
            request.Credentials = _credentials;
        }
    }
}

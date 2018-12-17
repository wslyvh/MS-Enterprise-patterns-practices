using System;

namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface IRestClient
    {
        IRestClientConfiguration RestClientConfiguration { get; }
        IRestRequest DefaultRestRequest { get; }
        Uri BaseUri { get; }
        IAuthenticator Authenticator { get; }
    }
}

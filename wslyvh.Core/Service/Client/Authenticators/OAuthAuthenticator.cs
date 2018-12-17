using System;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Authenticators
{
    public class OAuthAuthenticator : IAuthenticator
    {
        public OAuthAuthenticator()
        {
        }

        public virtual void Authenticate(IRestRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

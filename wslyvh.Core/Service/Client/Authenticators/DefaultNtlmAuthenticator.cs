using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using wslyvh.Core.Interfaces.ServiceClient;

namespace wslyvh.Core.Service.Client.Authenticators
{
    public class DefaultNtlmAuthenticator : NtlmAuthenticator
    {
        public DefaultNtlmAuthenticator()
            : base(CredentialCache.DefaultCredentials)
        {
        }
    }
}

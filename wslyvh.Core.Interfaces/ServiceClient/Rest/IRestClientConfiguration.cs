using System;
using System.Collections.Generic;
using wslyvh.Core.Interfaces.Serialization;

namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface IRestClientConfiguration
    {
        IAuthenticator Authenticator { get; }
        ISerializer Serializer { get; }

        Uri BaseUri { get; set; }
        string UserAgent { get; set; }
        string ContentType { get; set; }
        string Accept { get; set; }
        bool RequireSession { get; set; }
        bool PreAuthenticate { get; set; }

        IEnumerable<Parameter> DefaultHeaders { get; }
        IEnumerable<Parameter> DefaultParameters { get; }
        IEnumerable<Parameter> DefaultCookies { get; }
    }
}

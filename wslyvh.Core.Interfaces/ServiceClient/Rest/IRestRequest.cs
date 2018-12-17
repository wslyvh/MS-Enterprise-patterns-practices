using System;
using System.Collections.Generic;
using System.Net;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;

namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface IRestRequest
    {
        Uri BaseUri { get; set; }

        string Resource { get; set; }
        Method Method { get; set; }

        string UserAgent { get; set; }
        string ContentType { get; set; }
        string Accept { get; set; }
        bool RequireSession { get; set; }
        bool PreAuthenticate { get; set; }
        ICredentials Credentials { get; set; }

        IEnumerable<Parameter> Parameters { get; }
        IEnumerable<Parameter> Headers { get; }
        IEnumerable<Parameter> Cookies { get; }

        IRestRequest AddParameter(string name, object value);
        IRestRequest AddHeader(string name, string value);
        IRestRequest AddCookie(string name, string value);

        Uri BuildUri();
    }
}

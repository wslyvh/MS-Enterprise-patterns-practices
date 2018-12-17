using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Rest
{
    public class RestRequest : IRestRequest
    {
        private readonly List<Parameter> _parameters;
        private readonly List<Parameter> _headers;
        private readonly List<Parameter> _cookies;

        public Uri BaseUri { get; set; }
        public string Resource { get; set; }
        public Method Method { get; set; }
        public string UserAgent { get; set; }
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public bool RequireSession { get; set; }
        public bool PreAuthenticate { get; set; }
        public ICredentials Credentials { get; set; }

        public IEnumerable<Parameter> Parameters
        {
            get { return _parameters; }
        }

        public IEnumerable<Parameter> Headers
        {
            get { return _headers; }
        }

        public IEnumerable<Parameter> Cookies
        {
            get { return _cookies; }
        }
        
        public RestRequest()
        {
            _parameters = new List<Parameter>();
            _headers = new List<Parameter>();
            _cookies = new List<Parameter>();
        }

        public IRestRequest AddParameter(string name, object value)
        {
            _parameters.Add(new Parameter {Name = name, Value = value});
            return this;
        }

        public IRestRequest AddHeader(string name, string value)
        {
            _headers.Add(new Parameter { Name = name, Value = value });
            return this;
        }

        public IRestRequest AddCookie(string name, string value)
        {
            _cookies.Add(new Parameter { Name = name, Value = value });
            return this;
        }

        public Uri BuildUri()
        {
            var uri = new StringBuilder();
            var baseUri = BaseUri.ToString();

            uri.AppendFormat("{0}", baseUri.EndsWith("/") ? baseUri : baseUri + "/");
            uri.AppendFormat("{0}", Resource.StartsWith("/") ? Resource.Substring(1) : Resource);

            if (Parameters.Any())
            {
                var data = EncodeParameters(uri.ToString(), Parameters);
                uri.Append(data);
            }

            return new Uri(uri.ToString());
        }
        
        private string EncodeParameters(string baseUri, IEnumerable<Parameter> parameters)
        {
            var querystring = new StringBuilder();

            if (!baseUri.Contains("?"))
                querystring.Append("?");

            foreach (var p in parameters)
            {
                if (querystring.Length > 1 || (baseUri.Contains("?") || baseUri.Contains("&")))
                    querystring.Append("&");

                querystring.AppendFormat("{0}={1}", HttpUtility.UrlEncode(p.Name), HttpUtility.UrlEncode(p.Value.ToString()));
            }

            return querystring.ToString();
        }
    }
}

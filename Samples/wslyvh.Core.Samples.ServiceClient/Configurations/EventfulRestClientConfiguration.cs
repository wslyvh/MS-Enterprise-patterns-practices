using System;
using System.Collections.Generic;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Interfaces.ServiceClient.Rest;
using wslyvh.Core.Serialization;
using System.Linq;

namespace wslyvh.Core.Samples.ServiceClient.Configurations
{
    public class EventfulRestClientConfiguration : IRestClientConfiguration
    {
        private const string _baseUri = "http://api.eventful.com/rest/";
        private const string _apiKey = "key";
        private const string _contentType = "application/xml";

        public IAuthenticator Authenticator { get; private set; }
        public ISerializer Serializer { get; private set; }
        public Uri BaseUri { get; set; }
        public string UserAgent { get; set; }
        public string ContentType { get; set; }
        public string Accept { get; set; }
        public bool RequireSession { get; set; }
        public bool PreAuthenticate { get; set; }
        public IEnumerable<Parameter> DefaultHeaders { get; private set; }
        public IEnumerable<Parameter> DefaultParameters { get; private set; }
        public IEnumerable<Parameter> DefaultCookies { get; private set; }

        public EventfulRestClientConfiguration()
        {
            this.Serializer = new XmlDataSerializer();
            this.BaseUri = new Uri(_baseUri);
            this.UserAgent = _baseUri;
            this.ContentType = _contentType;
            this.Accept = _contentType;
            this.RequireSession = true;
            this.PreAuthenticate = false;
            this.DefaultHeaders = Enumerable.Empty<Parameter>();
            this.DefaultParameters = new List<Parameter>() { new Parameter() { Name = "app_key", Value = _apiKey } };
            this.DefaultCookies = Enumerable.Empty<Parameter>();
        }
    }
}

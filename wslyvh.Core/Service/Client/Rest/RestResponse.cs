using System;
using System.Net;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Rest
{
    public class RestResponse : IRestResponse
    {
        public IRestRequest Request { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string RawData { get; set; }
        public Uri ResponseUri { get; set; }
    }

    public class RestResponse<T> : RestResponse, IRestResponse<T>
    {
        public T Data { get; set; }
    }
}

﻿using System;
using System.Net;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;

namespace wslyvh.Core.Interfaces.ServiceClient.Rest
{
    public interface IHttpResponse
    {
        IRestRequest Request { get; set; }
        string ErrorMessage { get; set; }
        Exception Exception { get; set; }
        ResponseStatus ResponseStatus { get; set; }
        HttpStatusCode StatusCode { get; set; }
        string StatusDescription { get; set; }
        string RawData { get; set; }
        Uri ResponseUri { get; set; }
    }
}
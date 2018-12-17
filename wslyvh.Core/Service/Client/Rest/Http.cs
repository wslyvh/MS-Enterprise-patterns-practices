using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using wslyvh.Core.Interfaces;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Rest
{
    using System.Web;

    public class HttpFactory : IHttpFactory
    {
        public IHttp Create()
        {
            return new Http();
        }
    }

    public class Http : IHttp
    {
        private static readonly Dictionary<string, object> _cacheLocks = new Dictionary<string, object>();
        private static readonly Dictionary<string, CookieContainer> _containers = new Dictionary<string, CookieContainer>();

        #region IHttp Implementation
        public IHttpResponse GetResponse(IRestRequest restRequest)
        {
            var request = CreateWebRequest(restRequest);
            
            var response = new HttpResponse { Request = restRequest };

            try
            {
                using (var rawResponse = GetRawResponse(request))
                {
                    response = GetResponse(rawResponse);
                    
                    response.StatusCode = rawResponse.StatusCode;
                    response.StatusDescription = rawResponse.StatusDescription;
                    response.ResponseUri = rawResponse.ResponseUri;
                    response.ResponseStatus = ResponseStatus.Completed;
                }
            }
            catch (WebException webEx)
            {
                response.ErrorMessage = webEx.Message;
                response.Exception = webEx;
                response.StatusCode = ((HttpWebResponse) webEx.Response).StatusCode;
                response.StatusDescription = ((HttpWebResponse)webEx.Response).StatusDescription;
                response.ResponseUri = webEx.Response.ResponseUri;
                response.ResponseStatus = ResponseStatus.Error;
            }

            return response;
        }
        #endregion

        #region Private/Protected Implementation
        private HttpWebRequest CreateWebRequest(IRestRequest restRequest)
        {
            var uri = restRequest.BuildUri();
            var webRequest = (restRequest.RequireSession)
                              ? CreateWebRequest(uri)
                              : (HttpWebRequest)WebRequest.Create(uri);

            AppendHeaders(webRequest, restRequest.Headers);

            if (restRequest.RequireSession || restRequest.Cookies.Any())
                AppendCookies(webRequest, restRequest.Cookies);

            if (restRequest.Credentials != null)
                webRequest.Credentials = restRequest.Credentials;

            webRequest.Accept = restRequest.Accept;
            webRequest.ContentType = restRequest.ContentType;
            webRequest.Method = restRequest.Method.ToString();
            webRequest.UserAgent = restRequest.UserAgent;
            webRequest.PreAuthenticate = restRequest.PreAuthenticate;
            
            return webRequest;
        }

        private HttpWebRequest CreateWebRequest(Uri requestUri)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestUri);
            var domain = requestUri.GetLeftPart(UriPartial.Authority);

            CookieContainer container;
            var cacheLock = GetLockObject(requestUri.ToString());
            lock (cacheLock)
            {
                if (!_containers.TryGetValue(domain, out container))
                {
                    container = new CookieContainer();
                    _containers[domain] = container;
                }
            }

            request.CookieContainer = container;

            return request;
        }

        private void AppendHeaders(HttpWebRequest webRequest, IEnumerable<Parameter> headers)
        {
            foreach (var header in headers)
            {
                webRequest.Headers.Add(header.Name, header.Value.ToString());
            }
        }

        private void AppendCookies(HttpWebRequest webRequest, IEnumerable<Parameter> cookies)
        {
            if (webRequest.CookieContainer == null)
                webRequest.CookieContainer = new CookieContainer();

            foreach (var cookie in cookies)
            {
                webRequest.CookieContainer.Add(new Cookie(cookie.Name, cookie.Value.ToString())
                    {
                        Domain = webRequest.RequestUri.Host
                    });
            }
        }

        private static HttpWebResponse GetRawResponse(HttpWebRequest request)
        {
            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse)
                {
                    return ex.Response as HttpWebResponse;
                }

                throw;
            }
        }

        private HttpResponse GetResponse(HttpWebResponse webResponse)
        {
            var response = new HttpResponse { StatusCode = webResponse.StatusCode };

            var stream = webResponse.GetResponseStream();
            if (stream != null)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    response.RawData = streamReader.ReadToEnd();
                }
            }

            return response;
        }

        private static object GetLockObject(string key)
        {
            if (_cacheLocks.ContainsKey(key))
                return _cacheLocks[key];

            lock (_cacheLocks)
            {
                if (_cacheLocks.ContainsKey(key))
                    return _cacheLocks[key];

                _cacheLocks[key] = new object();
                return _cacheLocks[key];
            }
        }
        #endregion
    }
}

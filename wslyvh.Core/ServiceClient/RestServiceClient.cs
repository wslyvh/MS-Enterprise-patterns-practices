using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using wslyvh.Core.Interfaces.ServiceClient;

namespace wslyvh.Core.ServiceClient
{
    public class RestServiceClient : IServiceClient
    {
        private static readonly Dictionary<string, object> _cacheLocks = new Dictionary<string, object>();
        private static readonly Dictionary<string, CookieContainer> _containers = new Dictionary<string, CookieContainer>();
        private readonly IServiceClientConfiguration _configuration;
        //private delegate object ProcessRequestMethod();

        public RestServiceClient(IServiceClientConfiguration configuration)
        {
            Guard.ArgumentIsNotNull(configuration, "configuration");

            _configuration = configuration;
        }

        protected virtual HttpWebRequest Create(Uri uri)
        {
            Guard.ArgumentIsNotNull(uri, "uri");

            var request = (_configuration.RequireSession)
                              ? CreateRequest(uri)
                              : (HttpWebRequest) WebRequest.Create(uri);

            request.Accept = _configuration.Accept;
            request.ContentType = _configuration.ContentType;

            request = SetAuthentication(request);

            return request;
        }

        protected virtual HttpWebRequest SetAuthentication(HttpWebRequest request)
        {
            switch (_configuration.Authentication)
            {
                case Authentication.Default:
                    request.Credentials = CredentialCache.DefaultCredentials;
                    break;
                case Authentication.Basic:
                    var authorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(_configuration.AuthenticationToken));
                    request.Headers.Add("Authorization", "Basic " + authorization);
                    break;
            }

            return request;
        }

        #region IServiceClient Members
        public IResponse<TResponse> Get<TResponse>(Uri uri) where TResponse : class
        {
            Guard.ArgumentIsNotNull(uri, "uri");

            var request = Create(uri);
            var result = new Response<TResponse>();

            try
            {
                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
                    var stream = httpWebResponse.GetResponseStream();
                    if (stream == null)
                        return result;

                    string data;
                    using (var streamReader = new StreamReader(stream))
                    {
                        data = streamReader.ReadToEnd();
                    }

                    result.Data = _configuration.Serializer.Deserialize<TResponse>(data);
                    result.Status.Success = true;
                    result.Status.Message = "OK";
                    result.Status.Code = (int)httpWebResponse.StatusCode;
                }
            }
            catch (WebException webEx)
            {
                result.Status.Success = false;
                result.Status.Message = webEx.Message;
                result.Status.Code = (int)((HttpWebResponse)webEx.Response).StatusCode;
            }
            catch (HttpException httpEx)
            {
                result.Status.Success = false;
                result.Status.Message = httpEx.GetHtmlErrorMessage();
                result.Status.Code = httpEx.GetHttpCode();
            }
            catch (InvalidOperationException opEx)
            {
                result.Status.Success = false;
                result.Status.Message = string.Format("Error (de)serializing type: {0}. Exception: {1}", typeof(TResponse).Name, opEx.Message);
                result.Status.Code = -1;
            }

            return result;
        }

        public IResponse Put<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class
        {
            Guard.ArgumentIsNotNull(uri, "uri");
            Guard.ArgumentIsNotNull(data, "data");

            var request = Create(uri);
            request.Method = "PUT";
            var result = new Response();

            try
            {
                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
                    var requestData = _configuration.Serializer.Serialize(data);
                    var bytes = Encoding.UTF8.GetBytes(requestData);

                    using (var postStream = request.GetRequestStream())
                    {
                        postStream.Write(bytes, 0, bytes.Length);
                    }

                    result.Status.Success = true;
                    result.Status.Message = "OK";
                    result.Status.Code = (int)httpWebResponse.StatusCode;
                }
            }
            catch (WebException webEx)
            {
                result.Status.Success = false;
                result.Status.Message = webEx.Message;
                result.Status.Code = (int)((HttpWebResponse)webEx.Response).StatusCode;
            }
            catch (HttpException httpEx)
            {
                result.Status.Success = false;
                result.Status.Message = httpEx.GetHtmlErrorMessage();
                result.Status.Code = httpEx.GetHttpCode();
            }
            catch (InvalidOperationException opEx)
            {
                result.Status.Success = false;
                result.Status.Message = string.Format("Error (de)serializing type: {0}. Exception: {1}", typeof(TRequest).Name, opEx.Message);
                result.Status.Code = -1;
            }

            return result;
        }

        public IResponse Post<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class
        {
            Guard.ArgumentIsNotNull(uri, "uri");
            Guard.ArgumentIsNotNull(data, "data");

            var request = Create(uri);
            request.Method = "POST";
            var result = new Response();

            try
            {
                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
                    var requestData = _configuration.Serializer.Serialize(data);
                    var bytes = Encoding.UTF8.GetBytes(requestData);

                    using (var postStream = request.GetRequestStream())
                    {
                        postStream.Write(bytes, 0, bytes.Length);
                    }

                    result.Status.Success = true;
                    result.Status.Message = "OK";
                    result.Status.Code = (int)httpWebResponse.StatusCode;
                }
            }
            catch (WebException webEx)
            {
                result.Status.Success = false;
                result.Status.Message = webEx.Message;
                result.Status.Code = (int)((HttpWebResponse)webEx.Response).StatusCode;
            }
            catch (HttpException httpEx)
            {
                result.Status.Success = false;
                result.Status.Message = httpEx.GetHtmlErrorMessage();
                result.Status.Code = httpEx.GetHttpCode();
            }
            catch (InvalidOperationException opEx)
            {
                result.Status.Success = false;
                result.Status.Message = string.Format("Error (de)serializing type: {0}. Exception: {1}", typeof(TRequest).Name, opEx.Message);
                result.Status.Code = -1;
            }

            return result;
        }

        public IResponse Delete(Uri uri)
        {
            Guard.ArgumentIsNotNull(uri, "uri");

            var request = Create(uri);
            request.Method = "GET";
            var result = new Response();

            try
            {
                using (var httpWebResponse = (HttpWebResponse) request.GetResponse())
                {
                    var stream = httpWebResponse.GetResponseStream();
                    if (stream == null)
                        return result;

                    result.Status.Success = true;
                    result.Status.Message = string.Empty;
                    result.Status.Code = (int) httpWebResponse.StatusCode;
                }
            }
            catch (WebException webEx)
            {
                result.Status.Success = false;
                result.Status.Message = webEx.Message;
                result.Status.Code = (int)((HttpWebResponse)webEx.Response).StatusCode;
            }
            catch (HttpException httpEx)
            {
                result.Status.Success = false;
                result.Status.Message = httpEx.GetHtmlErrorMessage();
                result.Status.Code = httpEx.GetHttpCode();
            }

            return result;
        }
        #endregion

        #region Private methods
        private static HttpWebRequest CreateRequest(Uri uri)
        {
            var request = (HttpWebRequest)WebRequest.Create(uri);
            var domain = uri.GetLeftPart(UriPartial.Authority);

            CookieContainer container;
            var cacheLock = GetLockObject(uri.ToString());
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
        
        //private static IStatus ProcessRequest(ProcessRequestMethod method)
        //{
        //    var status = new Status();

        //    try
        //    {
        //        method.Invoke();
        //        status.Success = true;
        //        status.Message = "OK";
        //    }
        //    catch (WebException webEx)
        //    {
        //        status.Success = false;
        //        status.Message = webEx.Message;
        //        status.Code = (int)((HttpWebResponse)webEx.Response).StatusCode;
        //    }
        //    catch (HttpException httpEx)
        //    {
        //        status.Success = false;
        //        status.Message = httpEx.GetHtmlErrorMessage();
        //        status.Code = httpEx.GetHttpCode();
        //    }
        //    catch (InvalidOperationException opEx)
        //    {
        //        status.Success = false;
        //        //status.Message = string.Format("Error (de)serializing type: {0}. Exception: {1}", typeof(T).Name, opEx.Message);
        //        status.Message = string.Format("Error (de)serializing response. Exception: {0}", opEx.Message);
        //        status.Code = -1;
        //    }

        //    return status;
        //}
        
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

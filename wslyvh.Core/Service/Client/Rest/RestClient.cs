using System;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;
using wslyvh.Core.Interfaces.ServiceClient.Rest;

namespace wslyvh.Core.Service.Client.Rest
{
    public class RestClient : ISyncRestClient
    {
        #region Properties
        private readonly IHttpFactory _httpFactory;
        private IRestRequest _defaultRequest;

        public IRestClientConfiguration RestClientConfiguration { get; private set; }

        public IRestRequest DefaultRestRequest
        {
            get
            {
                if (_defaultRequest == null)
                    _defaultRequest = CreateDefaultRequest();

                return _defaultRequest;
            }
        }

        public Uri BaseUri
        {
            get { return RestClientConfiguration.BaseUri; }
        }

        public IAuthenticator Authenticator
        {
            get { return RestClientConfiguration.Authenticator; }
        }

        protected ISerializer Serializer
        {
            get { return RestClientConfiguration.Serializer; }
        }
        #endregion

        #region Ctor
        public RestClient(IHttpFactory httpFactory, IRestClientConfiguration restClientConfiguration)
        {
            Guard.ArgumentIsNotNull(httpFactory, "httpFactory");
            Guard.ArgumentIsNotNull(restClientConfiguration, "restClientConfiguration");

            _httpFactory = httpFactory;
            RestClientConfiguration = restClientConfiguration;
        }
        #endregion

        #region ISyncRestClient Implementation
        public IRestResponse Execute(Method method, string resource)
        {
            var request = DefaultRestRequest;
            request.Method = method;
            request.Resource = resource;

            return Execute(request);
        }

        public IRestResponse<T> Execute<T>(Method method, string resource) where T : class
        {
            var request = DefaultRestRequest;
            request.Method = method;
            request.Resource = resource;

            return Execute<T>(request);
        }

        public IRestResponse Execute(IRestRequest request)
        {
            AuthenticateIfNeeded(request);

            var http = _httpFactory.Create();
            var httpResponse = http.GetResponse(request);

            return ToRestReponse(httpResponse);
        }

        public IRestResponse<T> Execute<T>(IRestRequest request) where T : class
        {
            AuthenticateIfNeeded(request);

            var http = _httpFactory.Create();
            var httpResponse = http.GetResponse(request);

            return ToRestReponse<T>(httpResponse);
        }
        #endregion

        #region Private/Protected Implementation
        protected virtual IRestRequest CreateDefaultRequest()
        {
            var request = new RestRequest
                              {
                                  Accept = RestClientConfiguration.Accept,
                                  BaseUri = BaseUri,
                                  ContentType = RestClientConfiguration.ContentType,
                                  RequireSession = RestClientConfiguration.RequireSession,
                                  UserAgent = RestClientConfiguration.UserAgent,
                              };

            foreach (var parameter in RestClientConfiguration.DefaultParameters)
            {
                request.AddParameter(parameter.Name, parameter.Value);
            }

            foreach (var header in RestClientConfiguration.DefaultHeaders)
            {
                request.AddHeader(header.Name, header.Value.ToString());
            }

            foreach (var cookie in RestClientConfiguration.DefaultCookies)
            {
                request.AddHeader(cookie.Name, cookie.Value.ToString());
            }

            return request;
        }

        protected virtual void AuthenticateIfNeeded(IRestRequest request)
        {
            if (Authenticator != null)
                Authenticator.Authenticate(request);
        }

        protected virtual IRestResponse ToRestReponse(IHttpResponse httpResponse)
        {
            return new RestResponse
                {
                    Request = httpResponse.Request,
                    ErrorMessage = httpResponse.ErrorMessage,
                    Exception = httpResponse.Exception,
                    ResponseStatus = httpResponse.ResponseStatus,
                    StatusCode = httpResponse.StatusCode,
                    StatusDescription = httpResponse.StatusDescription,
                    RawData = httpResponse.RawData,
                    ResponseUri = httpResponse.ResponseUri,
                };
        }

        protected virtual IRestResponse<T> ToRestReponse<T>(IHttpResponse httpResponse) where T : class
        {
            var response = new RestResponse<T>
                {
                    Request = httpResponse.Request,
                    ErrorMessage = httpResponse.ErrorMessage,
                    Exception = httpResponse.Exception,
                    ResponseStatus = httpResponse.ResponseStatus,
                    StatusCode = httpResponse.StatusCode,
                    StatusDescription = httpResponse.StatusDescription,
                    RawData = httpResponse.RawData,
                    ResponseUri = httpResponse.ResponseUri,
                };

            if (!string.IsNullOrEmpty(httpResponse.RawData))
                response.Data = Serializer.Deserialize<T>(httpResponse.RawData);

            return response;
        }
        #endregion
    }
}

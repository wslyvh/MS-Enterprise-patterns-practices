using System;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.ServiceClient;

namespace wslyvh.Core.ServiceClient
{
    public class CachedServiceClient : IServiceClient
    {
        private readonly IServiceClient _serviceClient;
        private readonly ICachingProvider _cachingProvider;
        private readonly TimeSpan _defaultAbsoluteExpiration;
        private readonly string _baseCacheKey = "::ServiceClient.{0}.{1}-{2}";

        public CachedServiceClient(IServiceClient serviceClient, ICachingProvider cachingProvider)
            : this(serviceClient, cachingProvider, new TimeSpan(0, 0, 30, 0, 0))
        {
        }

        public CachedServiceClient(IServiceClient serviceClient, ICachingProvider cachingProvider, TimeSpan defaultAbsoluteExpiration)
        {
            Guard.ArgumentIsNotNull(serviceClient, "serviceClient");
            Guard.ArgumentIsNotNull(cachingProvider, "cachingProvider");

            _serviceClient = serviceClient;
            _cachingProvider = cachingProvider;
            _defaultAbsoluteExpiration = defaultAbsoluteExpiration;
        }

        #region IServiceClient Members
        public IResponse<TResponse> Get<TResponse>(Uri uri) where TResponse : class
        {
            var cacheKey = string.Format(_baseCacheKey, "Get", typeof (TResponse).Name, uri.PathAndQuery);
            return _cachingProvider.Retrieve(cacheKey, _defaultAbsoluteExpiration, () =>
                                                                                   _serviceClient.Get<TResponse>(uri));
        }

        public IResponse Put<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class
        {
            Guard.ArgumentIsNotNull(uri, "uri");
            Guard.ArgumentIsNotNull(data, "data");

            return _serviceClient.Put<TRequest>(uri, data);
        }

        public IResponse Post<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class
        {
            Guard.ArgumentIsNotNull(uri, "uri");
            Guard.ArgumentIsNotNull(data, "data");

            return _serviceClient.Post<TRequest>(uri, data);
        }

        public IResponse Delete(Uri uri)
        {
            Guard.ArgumentIsNotNull(uri, "uri");

            return _serviceClient.Delete(uri);
        }

        #endregion
    }
}

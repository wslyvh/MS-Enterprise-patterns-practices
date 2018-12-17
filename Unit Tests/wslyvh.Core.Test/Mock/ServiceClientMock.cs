using System;
using wslyvh.Core.Interfaces.ServiceClient;

namespace wslyvh.Core.Test.Mock
{
    public class ServiceClientMock : IServiceClient
    {
        public IResponse<TResponse> Get<TResponse>(Uri uri) where TResponse : class
        {
            return new Response<TResponse>();
        }

        public IResponse Put<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class
        {
            return new Response();
        }

        public IResponse Post<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class
        {
            return new Response();
        }

        public IResponse Delete(Uri uri)
        {
            return new Response();
        }
    }
}

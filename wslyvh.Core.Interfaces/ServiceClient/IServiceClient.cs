using System;

namespace wslyvh.Core.Interfaces.ServiceClient
{
    public interface IServiceClient
    {
        IResponse<TResponse> Get<TResponse>(Uri uri) where TResponse : class;
        IResponse Put<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class;
        IResponse Post<TRequest>(Uri uri, IRequest<TRequest> data) where TRequest : class;
        IResponse Delete(Uri uri);
    }
}

using System;
using System.ServiceModel;

namespace wslyvh.Core.Interfaces.ServiceModel
{
    public interface IServiceModelFactory<out TInterface> where TInterface : class
    {
        TInterface Create();
        TInterface Create(Uri remoteAddress);
        TInterface Create(Uri remoteAddress, Guid messageId);
        TInterface Create(Uri remoteAddress, Guid messageId, Uri replyTo);
        TInterface Create(Guid messageId);
        TInterface Create(Guid messageId, Uri replyTo);
        TInterface Create(string endpointName);
        TInterface Create(string endpointName, Uri remoteAddress);
        TInterface Create(string endpointName, EndpointAddress remoteEndpointAddress);
        TInterface Create(string endpointName, Uri remoteAddress, Guid messageId);
        TInterface Create(string endpointName, Uri remoteAddress, Guid messageId, Guid relatesTo);
        TInterface Create(string endpointName, Uri remoteAddress, Guid messageId, Uri replyTo);
        TInterface Create(string endpointName, Guid messageId);
        TInterface Create(string endpointName, Guid messageId, Uri replyTo);
    }
}

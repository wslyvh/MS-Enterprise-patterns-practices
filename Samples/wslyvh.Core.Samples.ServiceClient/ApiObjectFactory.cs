using System;

namespace wslyvh.Core.Samples.ServiceClient
{
    using wslyvh.Core.Samples.ServiceClient.Entities;
    using wslyvh.Core.Samples.ServiceClient.Repositories;

    public class ApiObjectFactory : IApiObjectFactory
    {
        private readonly IEventfulRepository _eventfulRepository;

        public ApiObjectFactory(IEventfulRepository eventfulRepository)
        {
            Guard.ArgumentIsNotNull(eventfulRepository, "eventfulRepository");
            
            _eventfulRepository = eventfulRepository;
        }

        public T Create<T>(string id) where T : class
        {
            if (typeof(T) == typeof(EventfulEvent))
            {
                return _eventfulRepository.GetEvent(id) as T;
            }

            throw new NotImplementedException("Creation of other (API) Event Types are not supported.");
        }
    }
}

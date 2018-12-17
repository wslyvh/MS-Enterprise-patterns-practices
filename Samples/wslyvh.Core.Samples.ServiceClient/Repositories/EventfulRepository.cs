using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;
using wslyvh.Core.Interfaces.ServiceClient.Rest;
using wslyvh.Core.Samples.ServiceClient.Entities;

namespace wslyvh.Core.Samples.ServiceClient.Repositories
{
    public class EventfulRepository : IEventfulRepository
    {
        private const int _defaultOrder = 1;
        private const int _defaultLimit = 10;

        private readonly ISyncRestClient _client;

        public EventfulRepository(ISyncRestClient client)
        {
            Guard.ArgumentIsNotNull(client, "client");

            this._client = client;
        }
        
        public EventfulEvent GetEvent(string id)
        {
            // Base Resource Uri
            var resource = string.Format("/events/{0}", id);

            // Execution
            var result = this._client.Execute<EventfulData>(Method.GET, resource);

            // Return
            if (result == null || result.Data == null || result.Data.Events == null)
                Console.WriteLine("No result.");
            else
                return result.Data.Events.FirstOrDefault();

            return null;
        }
    }
}

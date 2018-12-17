using System.Collections.Generic;
using wslyvh.Core.Samples.ServiceClient.Entities;

namespace wslyvh.Core.Samples.ServiceClient.Repositories
{
    public interface IEventfulRepository : IApiRepository
    {
        EventfulEvent GetEvent(string id);
    }
}
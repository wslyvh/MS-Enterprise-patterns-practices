using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wslyvh.Core.Interfaces.Serialization;

namespace wslyvh.Core.Interfaces.ServiceClient
{
    public interface IServiceClientConfiguration
    {
        string ContentType { get; set; }
        string Accept { get; set; }
        bool RequireSession { get; set; }
        Authentication Authentication { get; set; }
        string AuthenticationToken { get; set; }

        ISerializer Serializer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wslyvh.Core.Samples.BootMvp.Repositories
{
    public interface ISampleRepository
    {
        string GetMessage();
        void PostMessage(string message);
    }
}
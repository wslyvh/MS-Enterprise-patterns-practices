using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace wslyvh.Core.Samples.BootMvp.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        public string GetMessage()
        {
            return "Hello world";
        }

        public void PostMessage(string message)
        {
            Trace.WriteLine(message);
        }
    }
}
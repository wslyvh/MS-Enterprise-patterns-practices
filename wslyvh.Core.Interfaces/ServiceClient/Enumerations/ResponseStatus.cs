using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wslyvh.Core.Interfaces.ServiceClient.Enumerations
{
    /// <summary>
    /// Status for responses (surprised?)
    /// </summary>
    public enum ResponseStatus
    {
        None,
        Completed,
        Error,
        TimedOut,
        Aborted
    }
}

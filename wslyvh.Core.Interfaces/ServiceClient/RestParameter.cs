using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wslyvh.Core.Interfaces.ServiceClient.Enumerations;

namespace wslyvh.Core.Interfaces.ServiceClient
{
    public class RestParameter : Parameter
    {
        /// <summary>
        /// Type of the parameter
        /// </summary>
        public ParameterType Type { get; set; }
    }
}

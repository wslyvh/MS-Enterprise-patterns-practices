using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wslyvh.Core.Interfaces.ServiceClient.Enumerations
{        
    ///<summary>
    /// Types of parameters that can be added to requests
    ///</summary>
    public enum ParameterType
    {
        Cookie,
        GetOrPost,
        UrlSegment,
        HttpHeader,
        RequestBody,
        QueryString
    }
}

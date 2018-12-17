using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wslyvh.Core.Interfaces.ServiceClient.Enumerations
{
    public struct DateFormat
    {
        /// <summary>
        /// .NET format string for ISO 8601 date format
        /// </summary>
        public const string Iso8601 = "s";

        /// <summary>
        /// .NET format string for roundtrip date format
        /// </summary>
        public const string RoundTrip = "u";
    }
}

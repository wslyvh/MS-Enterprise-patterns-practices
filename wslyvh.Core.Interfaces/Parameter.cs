using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using wslyvh.Core;

namespace wslyvh.Core.Interfaces
{
    /// <summary>
    /// Parameter container for default Name/Value parameters
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Name of the parameter
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the parameter
        /// </summary>
        public object Value { get; set; }
        
        /// <summary>
        /// Return a human-readable representation of this parameter
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return string.Format("{0}={1}", Name, Value);
        }
    }
}

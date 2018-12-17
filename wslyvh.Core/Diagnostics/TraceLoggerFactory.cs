using System.Diagnostics;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Diagnostics
{
    public class TraceLoggerFactory : LoggerFactory
    {
        /// <summary>
        /// Creates a logger of the specified name with a specified default level.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        public override ILogger Create(string name, TraceEventType level)
        {
            return new TraceLogger(name, level);
        }
    }
}

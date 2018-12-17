using System;
using System.Diagnostics;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Diagnostics
{
    public class LoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Creates a logger of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public ILogger Create(Type type)
        {
            Guard.ArgumentIsNotNull(type, "type");
            return Create(type.FullName, TraceEventType.Information);
        }

        /// <summary>
        /// Creates a logger of the specified type with a specified default level.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public ILogger Create(Type type, TraceEventType level)
        {
            Guard.ArgumentIsNotNull(type, "type");
            return Create(type.FullName, level);
        }

        /// <summary>
        /// Creates a logger of the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public ILogger Create(string name)
        {
            Guard.ArgumentIsNotNull(name, "name");
            return Create(name, TraceEventType.Information);
        }

        /// <summary>
        /// Creates a logger of the specified name with a specified default level.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public virtual ILogger Create(string name, TraceEventType level)
        {
            Guard.ArgumentIsNotNull(name, "name");
            return new Logger(name, level);
        }
    }
}

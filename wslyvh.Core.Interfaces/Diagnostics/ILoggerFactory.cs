using System;
using System.Diagnostics;

namespace wslyvh.Core.Interfaces.Diagnostics
{
    /// <summary>
    /// Interface for a logger factory
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates a logger of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        ILogger Create(Type type);

        /// <summary>
        /// Creates a logger of the specified type with a specified default level.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        ILogger Create(Type type, TraceEventType level);
        
        /// <summary>
        /// Creates a logger of the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        ILogger Create(string name);

        /// <summary>
        /// Creates a logger of the specified name with a specified default level.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        ILogger Create(string name, TraceEventType level);
    }
}

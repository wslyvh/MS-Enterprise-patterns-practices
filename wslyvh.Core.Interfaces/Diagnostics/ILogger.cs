using System;
using System.Diagnostics;

namespace wslyvh.Core.Interfaces.Diagnostics
{
    /// <summary>
    /// Interface for a logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the default trace level.
        /// </summary>
        TraceEventType DefaultTraceLevel { get; }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Write(string message);

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        void Write(string message, TraceEventType level);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Write(Exception exception);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="level">The level.</param>
        void Write(Exception exception, TraceEventType level);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="handlingInstanceId">The handling instance id.</param>
        void Write(Exception exception, Guid handlingInstanceId);

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="handlingInstanceId">The handling instance id.</param>
        /// <param name="level">The level.</param>
        void Write(Exception exception, Guid handlingInstanceId, TraceEventType level);

    }
}

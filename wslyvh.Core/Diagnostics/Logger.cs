using System;
using System.Diagnostics;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Diagnostics
{
    public class Logger : ILogger
    {
        /// <summary>
        /// Gets the default trace level.
        /// </summary>
        public TraceEventType DefaultTraceLevel { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultTraceLevel">The default trace level.</param>
        public Logger(string name, TraceEventType defaultTraceLevel)
        {
            Name = name;
            DefaultTraceLevel = defaultTraceLevel;
        }

        #region ILogger members
        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void Write(string message)
        {
            Process(message, DefaultTraceLevel);
        }

        /// <summary>
        /// Writes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        public void Write(string message, TraceEventType level)
        {
            Process(message, level);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Write(Exception exception)
        {
            Process(exception, Guid.Empty, DefaultTraceLevel);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="level">The level.</param>
        public void Write(Exception exception, TraceEventType level)
        {
            Process(exception, Guid.Empty, level);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="handlingInstanceId">The handling instance id.</param>
        public void Write(Exception exception, Guid handlingInstanceId)
        {
            Process(exception, handlingInstanceId, DefaultTraceLevel);
        }

        /// <summary>
        /// Writes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="handlingInstanceId">The handling instance id.</param>
        /// <param name="level">The level.</param>
        public void Write(Exception exception, Guid handlingInstanceId, TraceEventType level)
        {
            Process(exception, handlingInstanceId, level);
        }
        #endregion

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        protected virtual void Process(string message, TraceEventType level)
        {
        }

        /// <summary>
        /// Processes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="handlingInstanceId">The handling instance id.</param>
        /// <param name="level">The level.</param>
        protected virtual void Process(Exception exception, Guid handlingInstanceId, TraceEventType level)
        {
        }
    }
}

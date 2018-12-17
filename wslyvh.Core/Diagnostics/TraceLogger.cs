using System;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace wslyvh.Core.Diagnostics
{
    public class TraceLogger : Logger
    {
        private const string _dateformat = @"yyyy/MM/dd HH:mm:ss";

        public TraceLogger(string name, TraceEventType defaultLevel)
            : base(name, defaultLevel)
        {
        }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="level">The level.</param>
        protected override void Process(string message, TraceEventType level)
        {
            base.Process(message, level);
            var textToLog = new StringBuilder();
            textToLog.AppendFormat("{0,-20} {1,-20} {2,-15} {3}", Name, DateTime.UtcNow.ToString(_dateformat, CultureInfo.InvariantCulture), string.Format("[{0}]", level.ToString().ToUpper()), message);

            Process(textToLog.ToString());
        }

        /// <summary>
        /// Processes the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="handlingInstanceId">The handling instance id.</param>
        /// <param name="level">The level.</param>
        /// <exception cref="System.ArgumentNullException">exception</exception>
        protected override void Process(Exception exception, Guid handlingInstanceId, TraceEventType level)
        {
            base.Process(exception, handlingInstanceId, level);
            Guard.ArgumentIsNotNull(exception, "exception");
            var message = new StringBuilder();
            message.AppendFormat("{0} ({1})", exception.Message, handlingInstanceId);
            message.AppendLine();
            message.Append(exception);

            Process(message.ToString(), level);
        }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void Process(string message)
        {
            Trace.WriteLine(message);
        }
    }
}

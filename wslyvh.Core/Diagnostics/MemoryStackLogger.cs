using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace wslyvh.Core.Diagnostics
{
    public class MemoryStackLogger : TraceLogger
    {
        public Stack<string> Log { get; private set; }

        public MemoryStackLogger(string name, TraceEventType defaultLevel)
            : base(name, defaultLevel)
        {
            Log = new Stack<string>();
        }

        /// <summary>
        /// Processes the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected override void Process(string message)
        {
            Log.Push(message);
        }
    }
}

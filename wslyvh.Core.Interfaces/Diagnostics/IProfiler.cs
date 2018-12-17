using System;

namespace wslyvh.Core.Interfaces.Diagnostics
{
    public interface IProfiler
    {
        /// <summary>
        /// Starts the Profiler timer
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the Profiler timer and calculates the execution time
        /// </summary>
        /// <returns>Execution time in milli seconds</returns>
        TimeSpan Stop();
    }
}

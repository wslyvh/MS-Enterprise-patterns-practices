using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Diagnostics
{
    public class Profiler : IProfiler
    {
        private readonly object _syncLock = new object();
        private readonly Dictionary<int, Stack<DateTime>> _profilePool = new Dictionary<int, Stack<DateTime>>();

        /// <summary>
        /// Starts the Profiler timer
        /// </summary>
        public void Start()
        {
            lock (_syncLock)
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;
                
                if (_profilePool.ContainsKey(currentThreadId))
                    _profilePool[currentThreadId].Push(DateTime.UtcNow);
                else
                {
                    var timerStack = new Stack<DateTime>();
                    timerStack.Push(DateTime.UtcNow);
                    _profilePool.Add(currentThreadId, timerStack);
                }
            }
        }

        /// <summary>
        /// Stops the Profiler timer and calculates the execution time
        /// </summary>
        /// <returns>Execution time in milli seconds</returns>
        public TimeSpan Stop()
        {
            lock (_syncLock)
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;
                var current = DateTime.UtcNow;

                if (_profilePool.ContainsKey(currentThreadId))
                {
                    var start = _profilePool[currentThreadId].Pop();

                    if (_profilePool[currentThreadId].Count == 0)
                        _profilePool.Remove(currentThreadId);

                    return current - start;
                }
            }

            return TimeSpan.Zero;
        }
    }
}

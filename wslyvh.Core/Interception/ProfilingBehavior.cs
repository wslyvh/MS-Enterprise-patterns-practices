using System;
using System.Diagnostics;
using Microsoft.Practices.Unity.InterceptionExtension;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Interception
{
    public class ProfilingBehavior : BaseInterceptionBehavior
    {
        private readonly ILogger _logger;
        private readonly IProfiler _profiler;

        public ProfilingBehavior(ILogger logger, IProfiler profiler)
        {
            Guard.ArgumentIsNotNull(logger, "logger");
            Guard.ArgumentIsNotNull(profiler, "profiler");

            _logger = logger;
            _profiler = profiler;
        }

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            _profiler.Start();
            
            var nextMethod = getNext().Invoke(input, getNext);

            var elapsedTime = _profiler.Stop();
            var duration = string.Format("{0:00}:{1:00}:{2:00}.{3:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds / 10);
            _logger.Write(string.Format("{0} {1} duration: {2}", input.Target, input.MethodBase, duration), TraceEventType.Verbose);

            return nextMethod;
        }
    }
}

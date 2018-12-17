using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Unity.InterceptionExtension;
using wslyvh.Core.Extensions;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Interception
{
    public class TraceBehavior : BaseInterceptionBehavior
    {
        private readonly ILogger _logger;

        public TraceBehavior(ILogger logger)
        {
            Guard.ArgumentIsNotNull(logger, "logger");

            _logger = logger;
        }

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            _logger.Write(string.Format("Entering Method invocation {0} {1}", input.Target, input.MethodBase), TraceEventType.Information);
            _logger.Write(string.Format("Arguments ({0}): {1}", input.MethodBase.GetParameters().Count(), input.GetMethodArgumentsIncludingTypeAsString(" | ")), TraceEventType.Information);
            
            var nextMethod = getNext().Invoke(input, getNext);

            if (nextMethod.Exception == null)
                _logger.Write(string.Format("Leaving Method invocation {0} {1}", input.Target, input.MethodBase), TraceEventType.Information);
            else
                _logger.Write(string.Format("Leaving Method invocation {0} {1} with exception {2}: {3}", input.Target, input.MethodBase, nextMethod.Exception.GetType().Name, nextMethod.Exception.Message), TraceEventType.Information);

            return nextMethod;
        }
    }
}

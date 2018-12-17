using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Diagnostics;
using wslyvh.Core.Extensions;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Interception
{
    /// <summary>
    /// CachingBehavior dynamically tries to retrieve the return value from Cache. If it doesn't exist, continue and insert it to the cache.
    /// CachingBehavior should be the last IInterceptionBehavior to return the value from the Cache immediately without continuing through the execution pipeline.
    /// </summary>
    public class CachingBehavior : BaseInterceptionBehavior
    {
        private const string _baseCacheKey = "wslyvh::";
        private readonly ICachingProvider _cachingProvider;
        private readonly ILogger _logger;

        public CachingBehavior(ICachingProvider cachingProvider, ILogger logger)
        {
            Guard.ArgumentIsNotNull(cachingProvider, "cachingProvider");
            Guard.ArgumentIsNotNull(logger, "logger");

            _cachingProvider = cachingProvider;
            _logger = logger;
        }

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            var cacheKey = GenerateCacheKey(input);
            _logger.Write(string.Format("Fetching {0} from {1}", cacheKey, _cachingProvider.GetType()), TraceEventType.Information);

            var result = _cachingProvider.Retrieve<object>(cacheKey);
            if (result == null)
            {
                _logger.Write(string.Format("{0} doesn't exist in Cache. Continue execution pipeline.", cacheKey), TraceEventType.Information);
                var methodReturn = getNext().Invoke(input, getNext);

                if (methodReturn.Exception == null && methodReturn.ReturnValue != null)
                {
                    _logger.Write(string.Format("Inserting {0}, with key: {1} to {2}", methodReturn.ReturnValue, cacheKey, _cachingProvider.GetType()), TraceEventType.Information);
                    _cachingProvider.Insert(cacheKey, methodReturn.ReturnValue);
                }
                else
                    _logger.Write(string.Format("Target {0} did not return a value, nothing to cache", input.Target), TraceEventType.Information);

                return methodReturn;
            }

            _logger.Write(string.Format("Object retrieved from {0}, value: {1}", _cachingProvider.GetType(), result), TraceEventType.Information);
            return input.CreateMethodReturn(result);
        }

        private static string GenerateCacheKey(IMethodInvocation input)
        {
            if (input.Arguments.Count == 0)
                return string.Format("{0}{1}", _baseCacheKey, input.Target);

            return string.Format("{0}{1}-{2}", _baseCacheKey, input.Target, input.GetMethodArgumentsAsString("|"));
        }
    }
}
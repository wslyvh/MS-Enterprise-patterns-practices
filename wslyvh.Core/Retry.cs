using System;
using System.Collections.Generic;
using System.Threading;

namespace wslyvh.Core
{
    public class Retry
    {
        private static readonly TimeSpan _defaultRetryInterval = TimeSpan.FromSeconds(1);
        private const int _defaultRetryCount = 3;

        public static void Do(Action action, TimeSpan? retryInterval, int retryCount = _defaultRetryCount)
        {
            Guard.ArgumentIsNotNull(action, "action");

            if (retryInterval == null)
                retryInterval = _defaultRetryInterval;

            Do<object>(() =>
            {
                action();
                return null;
            }, retryInterval, retryCount);
        }

        public static T Do<T>(Func<T> action, TimeSpan? retryInterval, int retryCount = 3)
        {
            Guard.ArgumentIsNotNull(action, "action");

            if (retryInterval == null)
                retryInterval = _defaultRetryInterval;

            var exceptions = new List<Exception>();
            for (int retry = 0; retry < retryCount; retry++)
            {
                try
                {
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                    Thread.Sleep(retryInterval.Value);
                }
            }

            throw new AggregateException(exceptions);
        }
    }
}

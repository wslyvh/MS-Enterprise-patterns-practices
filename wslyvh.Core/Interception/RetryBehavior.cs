using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace wslyvh.Core.Interception
{
    public class RetryBehavior : BaseInterceptionBehavior
    {
        private static readonly TimeSpan _retryInterval = TimeSpan.FromSeconds(1);
        private const int _retryCount = 2;

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            return Retry.Do(() => getNext().Invoke(input, getNext), _retryInterval, _retryCount);
        }
    }
}

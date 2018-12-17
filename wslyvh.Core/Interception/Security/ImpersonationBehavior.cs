using System;
using System.Security.Principal;
using System.Threading;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Interception.Security
{
    public class ImpersonationBehavior : BaseInterceptionBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            var callerWindowsIdentity = Thread.CurrentPrincipal.Identity as WindowsIdentity;
            if (callerWindowsIdentity == null)
                throw new InvalidOperationException("Current Thread Identity is not a valid Windows Identity.");

            IMethodReturn nextMethod;
            using (callerWindowsIdentity.Impersonate())
            {
                nextMethod = getNext().Invoke(input, getNext);
            }

            return nextMethod;
        }
    }
}

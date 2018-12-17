using System;
using System.Security.Principal;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Interception.Security
{
    public class ElevatedBehavior : BaseInterceptionBehavior
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Guard.ArgumentIsNotNull(input, "input");
            Guard.ArgumentIsNotNull(getNext, "getNext");

            IMethodReturn nextMethod;
            using (WindowsIdentity.Impersonate(IntPtr.Zero))
            {
                nextMethod = getNext().Invoke(input, getNext);
            }

            return nextMethod;
        }
    }
}

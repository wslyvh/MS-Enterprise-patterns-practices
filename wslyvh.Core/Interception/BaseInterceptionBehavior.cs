using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Interception
{
    public class BaseInterceptionBehavior : IInterceptionBehavior
    {
        public virtual IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            return getNext().Invoke(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}

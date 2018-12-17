using System;
using System.Linq;
using wslyvh.Core.Interfaces.Interception;

namespace wslyvh.Core.Interception.Rules
{
    public class DoesNotHaveGenericMethods : IInterceptRule
    {
        public bool Matches(Type typeToIntercept, Type typeOfInstance)
        {
            return typeOfInstance.GetMethods().Count(m => m.IsGenericMethod) == 0;
        }
    }
}

using System;

namespace wslyvh.Core.Interfaces.Interception
{
    public interface IInterceptRule
    {
        bool Matches(Type typeToIntercept, Type typeOfInstance);
    }
}

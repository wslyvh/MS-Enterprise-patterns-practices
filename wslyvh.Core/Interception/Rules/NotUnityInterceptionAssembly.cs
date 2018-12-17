using System;
using wslyvh.Core.Interfaces.Interception;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Interception.Rules
{
    public class NotUnityInterceptionAssembly : IInterceptRule
    {
        public bool Matches(Type typeToIntercept, Type typeOfInstance)
        {
            return !typeToIntercept.Assembly.Equals(typeof(unity.Interception).Assembly);
        }
    }
}

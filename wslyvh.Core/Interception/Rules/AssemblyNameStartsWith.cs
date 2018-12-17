using System;
using wslyvh.Core.Interfaces.Interception;

namespace wslyvh.Core.Interception.Rules
{
    public class AssemblyNameStartsWith : IInterceptRule
    {
        private string _match;

        public AssemblyNameStartsWith(string match)
        {
            Guard.ArgumentIsNotNull(match, "match");

            _match = match;
        }

        public bool Matches(Type typeToIntercept, Type typeOfInstance)
        {
            Guard.ArgumentIsNotNull(typeOfInstance, "typeOfInstance");

            return typeOfInstance.Assembly.FullName.StartsWith(_match);
        }
    }
}

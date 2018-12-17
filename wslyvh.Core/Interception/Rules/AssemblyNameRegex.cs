using System;
using wslyvh.Core.Interfaces.Interception;
using System.Text.RegularExpressions;

namespace wslyvh.Core.Interception.Rules
{
    public class AssemblyNameRegex : IInterceptRule
    {
        private Regex _pattern;

        public AssemblyNameRegex(Regex pattern)
        {
            Guard.ArgumentIsNotNull(pattern, "pattern");

            _pattern = pattern;
        }

        public bool Matches(Type typeToIntercept, Type typeOfInstance)
        {
            Guard.ArgumentIsNotNull(typeOfInstance, "typeOfInstance");

            return _pattern.IsMatch(typeOfInstance.Assembly.FullName);
        }
    }
}

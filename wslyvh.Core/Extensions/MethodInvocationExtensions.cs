using Microsoft.Practices.Unity.InterceptionExtension;
using System.Text;

namespace wslyvh.Core.Extensions
{
    public static class MethodInvocationExtensions
    {
        /// <summary>
        /// Iterates the MethodInvocation Arguments and returns it as a String in format:
        /// {Argument}={Value}
        /// </summary>
        /// <returns>{Argument}={Value}</returns>
        public static string GetMethodArgumentsAsString(this IMethodInvocation invocation, string seperator)
        {
            var sb = new StringBuilder();

            foreach (var parameter in invocation.MethodBase.GetParameters())
                sb.AppendFormat("{0}={1}{2}", parameter.Name, invocation.Arguments[parameter.Name], seperator);

            return sb.ToString().RemoveLastSeparator(seperator);
        }

        /// <summary>
        /// Iterates the MethodInvocation Arguments and returns it as a String in format:
        /// {Argument}={Value} ({Type}){Seperator}
        /// </summary>
        /// <returns>{Argument}={Value} ({Type}){Seperator}</returns>
        public static string GetMethodArgumentsIncludingTypeAsString(this IMethodInvocation invocation, string seperator)
        {
            var sb = new StringBuilder();

            foreach (var parameter in invocation.MethodBase.GetParameters())
                sb.AppendFormat("{0}={1} ({2}){3}", parameter.Name, invocation.Arguments[parameter.Name], parameter.ParameterType, seperator);

            return sb.ToString().RemoveLastSeparator(seperator);
        }
    }
}

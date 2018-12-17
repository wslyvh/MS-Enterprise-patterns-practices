using System;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Interception
{
    /// <summary>
    /// Generic ProxyGenerator
    /// </summary>
    /// <typeparam name="TInterceptionBehavior">The type of the interception behavior.</typeparam>
    /// <typeparam name="TInterface">The type of the interface.</typeparam>
    public class ProxyGenerator<TInterceptionBehavior, TInterface> where TInterceptionBehavior : class, IInterceptionBehavior
    {
        /// <summary>
        /// Creates an instance of TInterface.
        /// </summary>
        /// <returns></returns>
        public TInterface Create(ITypeInterceptor typeInterceptor, TInterceptionBehavior[] behaviors)
        {
            return (TInterface)Intercept.NewInstance<object>(typeInterceptor, behaviors);
        }

        /// <summary>
        /// Creates an instance of TInterface.
        /// </summary>
        /// <returns></returns>
        public TInterface Create(IInstanceInterceptor instanceInterceptor, Type target, TInterceptionBehavior[] behaviors)
        {
            return (TInterface)Intercept.ThroughProxy(
                Activator.CreateInstance(target),
                instanceInterceptor, behaviors);
        }
    }
}

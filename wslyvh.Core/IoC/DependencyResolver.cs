using System;
using System.Collections.Generic;
using wslyvh.Core.Interfaces.IoC;

namespace wslyvh.Core.IoC
{
    public class DependencyResolver
    {
        private static IDependencyResolver _resolver;

        /// <summary>
        /// Gets or sets a value indicating whether the Resolver is initialized with an IDependencyResolver.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public static bool IsInitialized { get; private set; }

        static DependencyResolver()
        {
            IsInitialized = false;
        }

        /// <summary>
        /// Initializes with the factory.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public static void InitializeWith(IDependencyResolverFactory factory)
        {
            _resolver = factory.CreateResolver();
            IsInitialized = true;
        }

        /// <summary>
        /// Registers the specified instance.
        /// </summary>
        public static void Register<T>(T instance) where T : class
        {
            _resolver.Register<T>(instance);
        }

        /// <summary>
        /// Gets the named instance.
        /// </summary>
        public static T GetNamedInstance<T>(string name)
        {
            return _resolver.GetNamedInstance<T>(name);
        }

        /// <summary>
        /// Resolves the specified type.
        /// 
        /// </summary>
        /// <typeparam name="T"/><param name="type">The type.</param>
        /// <returns/>
        public static T Resolve<T>(Type type)
        {
            return _resolver.Resolve<T>(type);
        }

        /// <summary>
        /// Resolves this instance.
        /// 
        /// </summary>
        /// <typeparam name="T"/>
        /// <returns/>
        public static T Resolve<T>()
        {
            return _resolver.Resolve<T>();
        }

        /// <summary>
        /// Resolves the specified type.
        /// 
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns/>
        public static object Resolve(Type type)
        {
            return _resolver.Resolve(type);
        }

        /// <summary>
        /// Resolves all specified types.
        /// 
        /// </summary>
        /// <typeparam name="T"/>
        /// <returns/>
        public static IEnumerable<T> ResolveAll<T>()
        {
            return _resolver.ResolveAll<T>();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            if (_resolver != null)
                _resolver.Dispose();

            IsInitialized = false;
        }
    }
}

using System;
using wslyvh.Core.Interfaces.IoC;

namespace wslyvh.Core.IoC
{
    public class DependencyResolverFactory : IDependencyResolverFactory
    {
        private readonly Type _resolverType;

        /// <summary>
        /// Initializes a new instance of the <see><cref>T:DependencyResolverFactory</cref></see> class.
        /// </summary>
        /// <param name="resolverTypeName">Name of the resolver type.</param>
        public DependencyResolverFactory(string resolverTypeName)
        {
            Guard.ArgumentIsNotNullOrEmpty(resolverTypeName, "resolverTypeName");
            _resolverType = Type.GetType(resolverTypeName, true, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see><cref>T:DependencyResolverFactory</cref></see> class.
        /// </summary>
        /// <param name="resolverType">Type of the resolver.</param>
        public DependencyResolverFactory(Type resolverType)
        {
            Guard.ArgumentIsNotNull(resolverType, "resolverType");
            _resolverType = resolverType;
        }

        /// <summary>
        /// Creates the <see><cref>T:IDependencyResolver</cref></see> .
        /// </summary>
        public IDependencyResolver CreateResolver()
        {
            Guard.ArgumentIsNotNull(_resolverType, "_resolverType");
            return Activator.CreateInstance(_resolverType) as IDependencyResolver;
        }
    }
}

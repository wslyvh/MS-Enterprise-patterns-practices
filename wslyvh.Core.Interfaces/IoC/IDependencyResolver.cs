using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wslyvh.Core.Interfaces.IoC
{
    public interface IDependencyResolver : IDisposable
    {
        /// <summary>
        /// Registers the specified instance.
        /// </summary>
        void Register<T>(T instance);

        /// <summary>
        /// Gets the named instance.
        /// </summary>
        T GetNamedInstance<T>(string name);

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        T Resolve<T>(Type type);

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        T Resolve<T>();

        /// <summary>
        /// Resolves the specified type.
        /// </summary>
        object Resolve(Type type);

        /// <summary>
        /// Resolves all.
        /// </summary>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Resets the specified instance.
        /// </summary>
        void Reset();
    }
}

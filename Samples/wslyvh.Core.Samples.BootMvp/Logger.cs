using System;
using System.Diagnostics;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Samples.BootMvp
{
    public class Logger : Singleton<Logger>
    {
        private Logger()
        {
            Init();
        }

        private IUnityContainer Container { get; set; }

        private void Init()
        {
            Container = ServiceLocator.Current.GetInstance<IUnityContainer>();
            var registrations = new RegisterTask { Container = Container };
            registrations.Execute();
        }

        /// <summary>
        /// Creates a logger of the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public ILogger Create(Type type)
        {
            return Instance.Container.Resolve<ILoggerFactory>().Create(type);
        }

        /// <summary>
        /// Creates a logger of the specified type with a specified default level.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">type</exception>
        public ILogger Create(Type type, TraceEventType level)
        {
            return Instance.Container.Resolve<ILoggerFactory>().Create(type, level);
        }

        /// <summary>
        /// Creates a logger of the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public ILogger Create(string name)
        {
            return Instance.Container.Resolve<ILoggerFactory>().Create(name);
        }

        /// <summary>
        /// Creates a logger of the specified name with a specified default level.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="level">The level.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public virtual ILogger Create(string name, TraceEventType level)
        {
            return Instance.Container.Resolve<ILoggerFactory>().Create(name, level);
        }
    }
}
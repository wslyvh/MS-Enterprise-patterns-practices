using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using wslyvh.Core.Interfaces.Boot;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Boot.Unity
{
    public class UnityBootstrapper : ConfigurableBootstrapper, IDisposable
    {
        private bool _initialized;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityBootstrapper" /> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IBootstrapperConfiguration" />.</param>
        public UnityBootstrapper(IBootstrapperConfiguration configuration)
            : base(configuration)
        {
            Guard.ArgumentIsNotNull(configuration, "configuration");

            Initialize();
            _initialized = true;
        }

        /// <summary>
        /// Gets the IUnityContainer.
        /// </summary>
        /// <value>The IUnityContainer.</value>
        public IUnityContainer Container { get; private set; }

        /// <summary>
        /// Adds an <see cref="IBootstrapperTask" /> to execute during start-up process.
        /// </summary>
        /// <param name="task">The <see cref="IBootstrapperTask" />.</param>
        public override void AddBootstrapperTasks(IBootstrapperTask task)
        {
            Guard.ArgumentIsNotNull(task, "task");

            Container.RegisterType(typeof(IBootstrapperTask), task.GetType(), task.GetType().FullName);
        }

        /// <summary>
        /// Initializes this <see cref="Bootstrapper" /> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Lifetime of the Container is controlled from this class and disposed as soon as this class will be disposed.")]
        protected sealed override void Initialize()
        {
            if (_initialized) return;

            Container = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));
        }

        /// <summary>
        /// Configures the <see cref="Bootstrapper" /> run based on the <see cref="IBootstrapperConfiguration" />.
        /// </summary>
        protected override void Configure()
        {
            var section = (UnityConfigurationSection)Configuration.GetConfiguration();
            if (section != null)
                section.Configure(Container);
        }

        /// <summary>
        /// Extends the <see cref="Bootstrapper" /> run.
        /// </summary>
        protected override void Extend()
        {
            // Extend the Container (e.g. extensions, builder strategies, interceptors, handlers, etc).
            Container.AddNewExtension<unity.Interception>();
        }

        /// <summary>
        /// Executes the <c>BootstrapperTasks</c>.
        /// </summary>
        protected override void ExecuteTasks()
        {
            foreach (var task in ServiceLocator.Current.GetAllInstances<IBootstrapperTask>())
                task.Execute();
        }

        /// <summary>
        /// Closes and disposes this <see cref="UnityBootstrapper" /> and any used resources.
        /// </summary>
        protected override void Close()
        {
            Dispose();
            _initialized = false;
        }

        #region IDisposable members
        ~UnityBootstrapper()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (!disposing)
                return;

            if (Container != null)
            {
                Container.Dispose();
                Container = null;
            }

            _disposed = true;
        }
        #endregion
    }
}

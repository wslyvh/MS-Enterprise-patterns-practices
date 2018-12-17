using System.Collections.Generic;
using wslyvh.Core.Interfaces.Boot;

namespace wslyvh.Core.Boot
{
    public abstract class Bootstrapper : IBootstrapper
    {
        private List<IBootstrapperTask> _bootstrapperTasks = new List<IBootstrapperTask>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Bootstrapper"/> class.
        /// </summary>
        protected Bootstrapper()
        {
        }
        
        /// <summary>
        /// Gets the collection of <see cref="IBootstrapperTask"/> to extend to the <see cref="Bootstrapper" />.
        /// </summary>
        /// <value>The <see cref="Bootstrapper" /> tasks.</value>
        protected IEnumerable<IBootstrapperTask> BootstrapperTasks
        {
            get { return _bootstrapperTasks; }
        }

        /// <summary>
        /// Starts up and runs the <see cref="Bootstrapper"/>.
        /// </summary>
        public void Startup()
        {
            Initialize();
            Configure();
            Extend();
            ExecuteTasks();
        }

        /// <summary>
        /// Shuts down the <see cref="Bootstrapper"/>.
        /// </summary>
        public void Shutdown()
        {
            Close();
        }

        /// <summary>
        /// Adds an <see cref="IBootstrapperTask" /> to execute during start-up process.
        /// </summary>
        /// <param name="task">The <see cref="IBootstrapperTask"/>.</param>
        public virtual void AddBootstrapperTasks(IBootstrapperTask task)
        {
            Guard.ArgumentIsNotNull(task, "task");

            _bootstrapperTasks.Add(task);
        }

        /// <summary>
        /// Initializes this <see cref="Bootstrapper"/> class.
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Configures the <see cref="Bootstrapper"/> run based on the <see cref="IBootstrapperConfiguration"/>.
        /// </summary>
        protected abstract void Configure();

        /// <summary>
        /// Extends the <see cref="Bootstrapper"/> run.
        /// </summary>
        protected abstract void Extend();

        /// <summary>
        /// Executes the <see cref="BootstrapperTasks"/>.
        /// </summary>
        protected virtual void ExecuteTasks()
        {
            foreach (var task in BootstrapperTasks)
                task.Execute();
        }

        /// <summary>
        /// Closes and disposes this <see cref="Bootstrapper"/> and any used resources.
        /// </summary>
        protected abstract void Close();
    }
}

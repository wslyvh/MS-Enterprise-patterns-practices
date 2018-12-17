
namespace wslyvh.Core.Interfaces.Boot
{
    /// <summary>
    /// Provides an interface for a generic bootstrapper that is responsible for starting and stopping an application or service.
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// Starts up and runs the bootstrapper.
        /// </summary>
        void Startup();

        /// <summary>
        /// Shuts down the bootstrapper.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Adds an <see cref="IBootstrapperTask" /> to execute during start-up process.
        /// </summary>
        /// <param name="task">The <see cref="IBootstrapperTask"/>.</param>
        void AddBootstrapperTasks(IBootstrapperTask task);
    }
}

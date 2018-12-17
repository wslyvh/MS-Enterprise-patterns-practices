
namespace wslyvh.Core.Interfaces.Boot
{
    /// <summary>
    /// Provides an interface for executing tasks during Bootstrapping.
    /// </summary>
    public interface IBootstrapperTask
    {
        /// <summary>
        /// Executes this IBootstrapperTask.
        /// </summary>
        void Execute();
    }
}

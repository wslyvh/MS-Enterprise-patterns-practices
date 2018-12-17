using wslyvh.Core.Interfaces.Boot;

namespace wslyvh.Core.Boot
{
    public abstract class ConfigurableBootstrapper : Bootstrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableBootstrapper" /> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IBootstrapperConfiguration" />.</param>
        protected ConfigurableBootstrapper(IBootstrapperConfiguration configuration)
        {
            Guard.ArgumentIsNotNull(configuration, "configuration");

            Configuration = configuration;
        }

        /// <summary>
        /// Gets the <see cref="Bootstrapper"/> configuration, to configure the <see cref="Bootstrapper"/> run.
        /// </summary>
        /// <value>The <see cref="Bootstrapper"/> Configuration.</value>
        protected IBootstrapperConfiguration Configuration { get; private set; }
    }
}

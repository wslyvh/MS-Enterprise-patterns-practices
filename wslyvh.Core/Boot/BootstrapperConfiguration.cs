using System.Configuration;
using wslyvh.Core.Interfaces.Boot;
using wslyvh.Core.Interfaces.Configuration;

namespace wslyvh.Core.Boot
{
    public abstract class BootstrapperConfiguration : IBootstrapperConfiguration
    {
        protected IConfigurationSource ConfigurationSource { get; private set; }

        public BootstrapperConfiguration(IConfigurationSource configurationSource)
        {
            Guard.ArgumentIsNotNull(configurationSource, "configurationSource");

            ConfigurationSource = configurationSource;
        }

        public abstract ConfigurationSection GetConfiguration();
    }
}

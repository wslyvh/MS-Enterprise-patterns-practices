using System.Configuration;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.Configuration;

namespace wslyvh.Core.Boot.Unity
{
    public class UnityBootstrapperConfiguration : BootstrapperConfiguration 
    {
        public UnityBootstrapperConfiguration(IConfigurationSource configurationSource)
            : base(configurationSource) { }

        public override ConfigurationSection GetConfiguration()
        {
            return ConfigurationSource.GetSection(SectionNames.Unity);
        }
    }
}

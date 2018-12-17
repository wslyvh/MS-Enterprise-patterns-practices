using system = System.Configuration;

namespace wslyvh.Core.Configuration.Source
{
    public class SystemConfigurationSource : FileConfigurationSource
    {
        protected override system.Configuration OpenConfiguration()
        {
            return system.ConfigurationManager.OpenExeConfiguration(system.ConfigurationUserLevel.None);
        }
    }
}

using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;

namespace wslyvh.Core.Test
{
    public static class BootstrapperHelper
    {
        public static void StartDefault()
        {
            var configSource = new SystemConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);
            var bootstrapper = new UnityBootstrapper(bootstrapperConfig);
            bootstrapper.Startup();
        }
    }
}

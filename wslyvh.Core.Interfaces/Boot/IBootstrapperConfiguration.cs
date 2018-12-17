using System.Configuration;

namespace wslyvh.Core.Interfaces.Boot
{
    /// <summary>
    /// Provides an interface for a bootstrapper configuration to configure the IoC container.
    /// </summary>
    public interface IBootstrapperConfiguration
    {
        ConfigurationSection GetConfiguration();
    }
}

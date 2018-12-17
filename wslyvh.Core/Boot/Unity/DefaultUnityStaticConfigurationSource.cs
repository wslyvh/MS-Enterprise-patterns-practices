using System.Collections.Generic;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using wslyvh.Core.Configuration.Source;

namespace wslyvh.Core.Boot.Unity
{
    public class DefaultUnityStaticConfigurationSource : StaticConfigurationSource 
    {
        protected override Dictionary<string, ConfigurationSection> OpenConfiguration()
        {
            var configuration = new Dictionary<string, ConfigurationSection>();
            var configSection = new UnityConfigurationSection();
            configSection.TypeAliases.Add(new AliasElement("singleton", typeof(ContainerControlledLifetimeManager)));
            configSection.TypeAliases.Add(new AliasElement("external", typeof(ExternallyControlledLifetimeManager)));
            configSection.Containers.Add(new ContainerElement());

            configuration.Add(SectionNames.Unity, configSection);

            return configuration;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using system = System.Configuration;

namespace wslyvh.Core.Configuration.Source
{
    public class StaticConfigurationSource : ConfigurationSource
    {
        private readonly object _lockObject = new object();
        private Dictionary<string, ConfigurationSection> _configurationSections;

        protected Dictionary<string, ConfigurationSection> ConfigurationSections
        {
            get
            {
                if (_configurationSections == null)
                {
                    lock (_lockObject)
                    {
                        _configurationSections = OpenConfiguration();
                    }
                }

                return _configurationSections;
            }
        }

        public void Add(string sectionName, ConfigurationSection section)
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");
            Guard.ArgumentIsNotNull(section, "section");

            ConfigurationSections.Add(sectionName, section);
        }

        public override ConfigurationSection GetSection(string sectionName)
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");

            return ConfigurationSections[sectionName];
        }

        public override TSection GetSection<TSection>(string sectionName)
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");

            return ConfigurationSections[sectionName] as TSection;
        }

        public override IEnumerable<ConfigurationSection> GetSections(Type sectionType)
        {
            Guard.ArgumentIsNotNull(sectionType, "sectionType");

            return ConfigurationSections.Values.Where(sectionType.IsInstanceOfType);
        }

        protected virtual Dictionary<string, ConfigurationSection> OpenConfiguration()
        {
            return new Dictionary<string, ConfigurationSection>();
        }
    }
}

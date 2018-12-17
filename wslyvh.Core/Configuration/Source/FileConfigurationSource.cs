using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using system = System.Configuration;

namespace wslyvh.Core.Configuration.Source
{
    public abstract class FileConfigurationSource : ConfigurationSource
    {
        private readonly object _lockObject = new object();
        private system.Configuration _configuration;

        protected system.Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    lock (_lockObject)
                    {
                        _configuration = OpenConfiguration();
                    }
                }

                return _configuration;
            }
        }

        public override ConfigurationSection GetSection(string sectionName)
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");

            return Configuration.GetSection(sectionName);
        }

        public override TSection GetSection<TSection>(string sectionName)
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");

            return Configuration.GetSection(sectionName) as TSection;
        }

        public override IEnumerable<ConfigurationSection> GetSections(Type sectionType)
        {
            Guard.ArgumentIsNotNull(sectionType, "sectionType");

            return Configuration.Sections.Cast<ConfigurationSection>().Where(section => sectionType.IsAssignableFrom(section.ElementInformation.Type));
        }

        protected abstract system.Configuration OpenConfiguration();
    }
}

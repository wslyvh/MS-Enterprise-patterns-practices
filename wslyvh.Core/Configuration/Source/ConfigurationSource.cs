using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using wslyvh.Core.Interfaces.Configuration;

namespace wslyvh.Core.Configuration.Source
{
    public abstract class ConfigurationSource : IConfigurationSource
    {
        public bool ContainsSection(string sectionName)
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");

            return GetSection(sectionName) != null;
        }

        public bool ContainsSection<TSection>(string sectionName) where TSection : ConfigurationSection
        {
            Guard.ArgumentIsNotNullOrEmpty(sectionName, "sectionName");

            return GetSection<TSection>(sectionName) != null;
        }

        public abstract ConfigurationSection GetSection(string sectionName);

        public abstract TSection GetSection<TSection>(string sectionName) where TSection : ConfigurationSection;

        public abstract IEnumerable<ConfigurationSection> GetSections(Type sectionType);

        public IEnumerable<TSection> GetSections<TSection>() where TSection : ConfigurationSection
        {
            return GetSections(typeof(TSection)).Cast<TSection>();
        }
    }
}
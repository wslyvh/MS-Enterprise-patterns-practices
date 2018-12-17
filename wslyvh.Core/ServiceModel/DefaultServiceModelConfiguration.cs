using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using wslyvh.Core.Interfaces.ServiceModel;

namespace wslyvh.Core.ServiceModel
{
    using wslyvh.Core.Configuration.Source;

    public class DefaultServiceModelConfiguration : IServiceModelConfiguration
    {
        public string ServiceBindingsSetting { get; private set; }
        public string ServiceClientSetting { get; private set; }
        public string ServiceBehaviorsSetting { get; private set; }
        public string ServiceDiagnosticsSetting { get; private set; }

        public DefaultServiceModelConfiguration()
        {
            CreateConfiguration();
        }

        private void CreateConfiguration()
        {
            var serviceModelSectionGroup = ConfigurationManager.GetSection(SectionNames.ServiceModel) as ServiceModelSectionGroup;
            if (serviceModelSectionGroup == null)
                throw new InvalidOperationException(
                    string.Format("Unable to retrieve {0} sectiongroup from default *.config file", SectionNames.ServiceModel));

            ServiceBindingsSetting = serviceModelSectionGroup.Bindings.SectionInformation.GetRawXml();
            ServiceClientSetting = serviceModelSectionGroup.Client.SectionInformation.GetRawXml();
            ServiceBehaviorsSetting = serviceModelSectionGroup.Behaviors.SectionInformation.GetRawXml();
            ServiceDiagnosticsSetting = serviceModelSectionGroup.Diagnostic.SectionInformation.GetRawXml();
        }
    }
}

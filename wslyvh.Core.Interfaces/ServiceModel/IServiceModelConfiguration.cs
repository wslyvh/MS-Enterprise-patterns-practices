
namespace wslyvh.Core.Interfaces.ServiceModel
{
    public interface IServiceModelConfiguration
    {
        string ServiceBindingsSetting { get; }
        string ServiceClientSetting { get; }
        string ServiceBehaviorsSetting { get; }
        string ServiceDiagnosticsSetting { get; }
    }
}

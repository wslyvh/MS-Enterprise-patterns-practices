
namespace wslyvh.Core.Interfaces.Configuration
{
    public interface IConfigurationProvider
    {
        string Get(string key);
        T Get<T>(string key);
    }
}

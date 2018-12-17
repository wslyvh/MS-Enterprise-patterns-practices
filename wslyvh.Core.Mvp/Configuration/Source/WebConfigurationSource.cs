using System.Web.Configuration;
using wslyvh.Core.Configuration.Source;
using system = System.Configuration;

namespace wslyvh.Core.Web.Configuration.Source
{
    public class WebConfigurationSource : FileConfigurationSource
    {
        protected override system.Configuration OpenConfiguration()
        {
            return WebConfigurationManager.OpenWebConfiguration("~/");
        }
    }
}

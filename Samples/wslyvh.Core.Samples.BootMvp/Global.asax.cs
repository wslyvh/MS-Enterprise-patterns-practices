using System;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Web.Configuration.Source;

namespace wslyvh.Core.Samples.BootMvp
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var configSource = new WebConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);
            var bootstrapper = new UnityBootstrapper(bootstrapperConfig);
            bootstrapper.Startup();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.Boot;
using wslyvh.Core.Web.Configuration.Source;

namespace wslyvh.Core.Sample
{
    public class Global : System.Web.HttpApplication
    {
        IBootstrapper bootstrapper;

        void Application_Start(object sender, EventArgs e)
        {
            var configSource = new WebConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);

            bootstrapper = new UnityBootstrapper(bootstrapperConfig);

            bootstrapper.Startup();
        }

        void Application_End(object sender, EventArgs e)
        {
            bootstrapper.Shutdown();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}

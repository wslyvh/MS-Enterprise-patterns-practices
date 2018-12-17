using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Test.Boot
{
    using wslyvh.Core.Configuration.Source;

    [TestClass]
    public class BootstrapperTest
    {
        [TestMethod]
        public void BootstrapperStartupTest()
        {
            var configSource = new SystemConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);
            var bootstrapper = new UnityBootstrapper(bootstrapperConfig);
            bootstrapper.AddBootstrapperTasks(new BootstrapperTask());
            
            bootstrapper.Startup();

            var expected = typeof (TraceLoggerFactory);
            var actual = ServiceLocator.Current.GetInstance<ILoggerFactory>().GetType();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BootstrapperNullTest()
        {
            var bootstrapper = new UnityBootstrapper(null);
        }

        [TestMethod]
        public void BootstrapperShutdownTest()
        {
            var configSource = new DefaultUnityStaticConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);
            var bootstrapper = new UnityBootstrapper(bootstrapperConfig);

            bootstrapper.Startup();
            
            bootstrapper.Shutdown();
        }
    }
}

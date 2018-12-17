using System;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.Configuration;
using wslyvh.Core.Interfaces.Diagnostics;

namespace wslyvh.Core.Samples
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var configSource = new SystemConfigurationSource();
            var bootstrapperConfig = new UnityBootstrapperConfiguration(configSource);
            var bootstrapper = new UnityBootstrapper(bootstrapperConfig);
            bootstrapper.Startup();

            WriteContainerInfo();

            //LoggerSample();
            //SettingSample();
            CachedSettingBehaviorSample();
            //FindTypes();

            WriteContainerInfo();

            Console.ReadLine();
        }

        private static void WriteContainerInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Container Info\n-----");
            Console.WriteLine();

            Console.WriteLine("Registrations: ");
        }

        private static void LoggerSample()
        {
            Console.WriteLine();
            Console.WriteLine("LoggerSample\n-----");
            Console.WriteLine();

            var logger = ServiceLocator.Current.GetInstance<ILoggerFactory>().Create("Trace Logger");
            logger.Write("Test Log message");
        }

        private static void SettingSample()
        {
            Console.WriteLine();
            Console.WriteLine("SettingSample\n-----");
            Console.WriteLine();

            var provider = ServiceLocator.Current.GetInstance<ISettingProvider>();

            var boolSetting = provider.Get<bool>("boolSetting");
            Console.WriteLine("boolSetting value: {0} of Type: {1}", boolSetting, typeof(bool));

            //Try / do nothing to log the exception at the TraceBehavior 
            try
            {
                var nullSetting = provider.Get<string>(null);
            }
            catch
            {
            }

            var stringSetting = provider.Get<string>("stringSetting");
            Console.WriteLine("stringSetting value: {0} of Type: {1}", stringSetting, typeof(string));

            var intSetting = provider.Get<int>("intSetting");
            Console.WriteLine("intSetting value: {0} of Type: {1}", intSetting, typeof(int));

            boolSetting = provider.Update<bool>("boolSetting", "false");
            Console.WriteLine("Update boolSetting value: {0} of Type: {1}", boolSetting, typeof(bool));
        }

        private static void CachedSettingProviderSample()
        {
            Console.WriteLine();
            Console.WriteLine("CachedSettingProviderSample\n-----");
            Console.WriteLine();

            var provider = ServiceLocator.Current.GetInstance<ISettingProvider>("CachedSettingProvider");

            var stringSetting = provider.Get<string>("stringSetting");
            Console.WriteLine("stringSetting value: {0} of Type: {1}", stringSetting, typeof(string));

            //Call 2nd time to check Cache

            stringSetting = provider.Get<string>("stringSetting");
            Console.WriteLine("stringSetting value: {0} of Type: {1}", stringSetting, typeof(string));
        }

        private static void CachedSettingBehaviorSample()
        {
            Console.WriteLine();
            Console.WriteLine("CachedSettingBehaviorSample\n-----");
            Console.WriteLine();

            var provider = ServiceLocator.Current.GetInstance<ISettingProvider>();

            var stringSetting = provider.Get<string>("stringSetting");
            Console.WriteLine("stringSetting value: {0} of Type: {1}", stringSetting, typeof(string));

            //Call 2nd time to check Cache

            stringSetting = provider.Get<string>("stringSetting");
            Console.WriteLine("stringSetting value: {0} of Type: {1}", stringSetting, typeof(string));
        }

        private static void FindTypes()
        {
            Console.WriteLine();
            Console.WriteLine("FindTypes\n-----");
            Console.WriteLine();

            var type = typeof(ISettingProvider);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) && p.IsClass && !p.Namespace.Equals("DynamicModule.ns"));

            Console.WriteLine("Types implementing ISettingProvider in current AppDomain ");
            foreach (var t in types)
            {
                Console.WriteLine("\t- {0}", t.FullName);
            }

            Console.WriteLine();
            Console.WriteLine("Types implementing ISettingProvider from ServiceLocator ");
            foreach (var provider in ServiceLocator.Current.GetAllInstances<ISettingProvider>())
                Console.WriteLine("\t- {0}", provider.GetType().FullName);
        }
    }
}

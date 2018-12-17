using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.BackingStoreImplementations;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Caching;
using wslyvh.Core.Configuration;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Extensions;
using wslyvh.Core.Extensions.Container;
using wslyvh.Core.Interception.Rules;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.Configuration;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Interfaces.Interception;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Interfaces.ServiceClient;
using wslyvh.Core.Serialization;
using wslyvh.Core.ServiceClient;
using wslyvh.Core.Web.Caching;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Test
{
    public class BootstrapperTask : UnityBootstrapperTask
    {
        public override void Execute()
        {
            RegisterMatchingRules();
            RegisterExtensions();
            RegisterTypes();
        }

        private void RegisterMatchingRules()
        {
            if (!Container.IsRegistered<IInterceptRule>("NotUnityInterceptionAssembly"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IInterceptRule, NotUnityInterceptionAssembly>("NotUnityInterceptionAssembly", lifetime);

            if (!Container.IsRegistered<IInterceptRule>("AssemblyNameStartsWith_wslyvh"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IInterceptRule, AssemblyNameStartsWith>("AssemblyNameStartsWith_wslyvh", lifetime,
                        new InjectionConstructor("wslyvh."));
        }

        private void RegisterExtensions()
        {
            Container.AddNewExtension<unity.Interception>();
            Container.AddNewExtension<AutoMoqContainerExtension>();
        }

        private void RegisterTypes()
        {
            if (!Container.IsRegistered<ILoggerFactory>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ILoggerFactory, TraceLoggerFactory>(lifetime);

            if (!Container.IsRegistered<ISettingProvider>())
                Container.RegisterType<ISettingProvider, AppSettingProvider>();

            if (!Container.IsRegistered<ILogger>("MemoryStackLogger"))
                Container.RegisterType<ILogger, MemoryStackLogger>("MemoryStackLogger",
                    new InjectionConstructor("MemoryStackLogger", TraceEventType.Information));

            if (!Container.IsRegistered<ISerializer>("XmlDataSerializer"))
                Container.RegisterType<ISerializer, XmlDataSerializer>("XmlDataSerializer");

            if (!Container.IsRegistered<ISerializer>("DataSerializer"))
                Container.RegisterType<ISerializer, DataSerializer>("DataSerializer");

            if (!Container.IsRegistered<ISerializer>("JsonSerializer"))
                Container.RegisterType<ISerializer, JsonSerializer>("JsonSerializer");

            if (!Container.IsRegistered<ICachingProvider>("HttpRuntimeCachingProvider"))
                Container.RegisterType<ICachingProvider, HttpRuntimeCachingProvider>("HttpRuntimeCachingProvider");

            if (!Container.IsRegistered<ICacheManager>())
                Container.RegisterInstance<ICacheManager>(CreateEntLibCacheManager());

            if (!Container.IsRegistered<ICachingProvider>("EntLibCachingProvider"))
                Container.RegisterType<ICachingProvider, EntLibCachingProvider>("EntLibCachingProvider");

            //if (!Container.IsRegistered<IServiceClient>("RestServiceClient"))
            //    Container.RegisterType<IServiceClient, ServiceClientMock>("RestServiceClient");

            if (!Container.IsRegistered<IServiceClient>("RestServiceClient"))
                Container.RegisterMock<IServiceClient>("RestServiceClient");

            if (!Container.IsRegistered<IServiceClient>("CachedServiceClient"))
                Container.RegisterType<IServiceClient, CachedServiceClient>("CachedServiceClient",
                    new InjectionConstructor(
                        Container.Resolve<IServiceClient>("RestServiceClient"),
                        Container.Resolve<ICachingProvider>("HttpRuntimeCachingProvider")));
        }

        private ICacheManager CreateEntLibCacheManager()
        {
            var internalConfigurationSource = new DictionaryConfigurationSource();
            var settings = new CacheManagerSettings();
            internalConfigurationSource.Add(CacheManagerSettings.SectionName, settings);

            var storageConfig = new CacheStorageData("Null Storage", typeof(NullBackingStore));
            settings.BackingStores.Add(storageConfig);

            var cacheManagerConfig = new CacheManagerData("CustomCache", 60, 1000, 10, storageConfig.Name);
            settings.CacheManagers.Add(cacheManagerConfig);
            settings.DefaultCacheManager = cacheManagerConfig.Name;
            
            return new CacheManagerFactory(internalConfigurationSource).CreateDefault();
        }
    }
}

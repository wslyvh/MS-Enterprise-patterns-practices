using System.Diagnostics;
using Microsoft.Practices.Unity;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Extensions.Container;
using wslyvh.Core.Interception;
using wslyvh.Core.Interception.Rules;
using wslyvh.Core.Interception.Security;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.Configuration;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Interfaces.Interception;
using wslyvh.Core.Web.Caching;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Samples
{
    public class RegisterTask : UnityBootstrapperTask
    {
        #region IRegisterTask Members
        public override void Execute()
        {
            RegisterMatchingRules();
            RegisterExtensions();
            RegisterLoggers();
            RegisterBehaviors();
            RegisterProviders();
        }
        #endregion

        #region Private Registration Methods
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

            Container.AddNewExtension<InterceptionContainerExtension>()
                     .Configure<InterceptionContainerExtension>()
                     .SetDefaultInterceptor<unity.InterfaceInterceptor>()
                     .AddMatchingRule(Container.Resolve<IInterceptRule>("NotUnityInterceptionAssembly"))
                     .AddMatchingRule(Container.Resolve<IInterceptRule>("AssemblyNameStartsWith_wslyvh"));
        }

        private void RegisterLoggers()
        {
            if (!Container.IsRegistered<ILoggerFactory>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ILoggerFactory, TraceLoggerFactory>(lifetime);

            if (!Container.IsRegistered<ILogger>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ILogger, TraceLogger>(lifetime, new InjectionConstructor("Default Trace Logger", TraceEventType.Information));

            if (!Container.IsRegistered<IProfiler>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IProfiler, Profiler>(lifetime);

            if (!Container.IsRegistered<ICachingProvider>())
                Container.RegisterType<ICachingProvider, HttpRuntimeCachingProvider>();

        }

        private void RegisterBehaviors()
        {
            if (!Container.IsRegistered<unity.IInterceptionBehavior>("TraceBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, TraceBehavior>("TraceBehavior", lifetime);

            if (!Container.IsRegistered<unity.IInterceptionBehavior>("TraceIdentityBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, TraceIdentityBehavior>("TraceIdentityBehavior", lifetime);

            if (!Container.IsRegistered<unity.IInterceptionBehavior>("ClaimsToWindowsTokenBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, ClaimsToWindowsTokenBehavior>("ClaimsToWindowsTokenBehavior", lifetime);

            if (!Container.IsRegistered<unity.IInterceptionBehavior>("ElevatedBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, ElevatedBehavior>("ElevatedBehavior", lifetime);

            if (!Container.IsRegistered<unity.IInterceptionBehavior>("ImpersonationBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, ImpersonationBehavior>("ImpersonationBehavior", lifetime);

            if (!Container.IsRegistered<unity.IInterceptionBehavior>("ProfilingBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, ProfilingBehavior>("ProfilingBehavior", lifetime);

            if (!Container.IsRegistered<unity.IInterceptionBehavior>("CachingBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<unity.IInterceptionBehavior, CachingBehavior>("CachingBehavior", lifetime);

            Container.AddNewExtension<BehaviorContainerExtension>()
                     .Configure<BehaviorContainerExtension>()
                     .SetDefaultInterceptor<unity.InterfaceInterceptor>()
                     .AddBehavior(Container.Resolve<unity.IInterceptionBehavior>("ProfilingBehavior"))
                     .AddBehavior(Container.Resolve<unity.IInterceptionBehavior>("ElevatedBehavior"))
                     .AddBehavior(Container.Resolve<unity.IInterceptionBehavior>("TraceIdentityBehavior"))
                     .AddBehavior(Container.Resolve<unity.IInterceptionBehavior>("TraceBehavior"))
                     .AddBehavior(Container.Resolve<unity.IInterceptionBehavior>("CachingBehavior"))
                     .AddMatchingRule(Container.Resolve<IInterceptRule>("NotUnityInterceptionAssembly"))
                     .AddMatchingRule(Container.Resolve<IInterceptRule>("AssemblyNameStartsWith_wslyvh"));
        }

        private void RegisterProviders()
        {
            if (!Container.IsRegistered<ISettingProvider>())
                Container.RegisterType<ISettingProvider, AppSettingProvider>();

            if (!Container.IsRegistered<ICachingProvider>())
                Container.RegisterType<ICachingProvider, HttpRuntimeCachingProvider>();

            if (!Container.IsRegistered<ISettingProvider>("CachedSettingProvider"))
                Container.RegisterType<ISettingProvider, CachedSettingProvider>("CachedSettingProvider",
                    new InjectionConstructor(typeof(ISettingProvider), typeof(ICachingProvider)));
        }
        #endregion
    }
}

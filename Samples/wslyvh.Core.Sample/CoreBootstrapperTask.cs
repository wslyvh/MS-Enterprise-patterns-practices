﻿using System.Diagnostics;
using Microsoft.Practices.Unity;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Extensions.Container;
using wslyvh.Core.Interception;
using wslyvh.Core.Interception.Rules;
using wslyvh.Core.Interception.Security;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Interfaces.Interception;
using wslyvh.Core.Web.Caching;
using wslyvh.Core.Web.Context;
using unity = Microsoft.Practices.Unity.InterceptionExtension;

namespace wslyvh.Core.Sample
{
    public class CoreBootstrapperTask : UnityBootstrapperTask
    {
        public override void Execute()
        {
            CoreContext.Current.Items[ContextKeys.Container] = this.Container;

            RegisterRules();
            RegisterExtensions();
            RegisterCoreProviders();
            RegisterBehaviors();
        }

        private void RegisterRules()
        {
            // Matching Rules
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
            // Extensions
            Container.AddNewExtension<InterceptionContainerExtension>()
                     .Configure<InterceptionContainerExtension>()
                     .SetDefaultInterceptor<unity.InterfaceInterceptor>()
                     .AddMatchingRule(Container.Resolve<IInterceptRule>("NotUnityInterceptionAssembly"))
                     .AddMatchingRule(Container.Resolve<IInterceptRule>("AssemblyNameStartsWith_wslyvh"));
        }

        private void RegisterCoreProviders()
        {
            // Loggers / Providers
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
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ICachingProvider, HttpRuntimeCachingProvider>(lifetime);
        }

        private void RegisterBehaviors()
        {
            // Behaviors
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
    }
}
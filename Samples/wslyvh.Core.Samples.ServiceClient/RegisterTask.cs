using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Interception;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Interfaces.ServiceClient.Rest;
using wslyvh.Core.Serialization;
using wslyvh.Core.Service.Client.Rest;

namespace wslyvh.Core.Samples.ServiceClient
{
    using System.Diagnostics;

    using wslyvh.Core.Samples.ServiceClient.Configurations;
    using wslyvh.Core.Samples.ServiceClient.Repositories;

    public class RegisterTask : UnityBootstrapperTask
    {
        public override void Execute()
        {
            var loggerFactory = new TraceLoggerFactory();
            var logger = loggerFactory.Create("Default");

            if (!Container.IsRegistered<ILoggerFactory>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterInstance(loggerFactory, lifetime);

            if (!Container.IsRegistered<ILogger>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ILogger, TraceLogger>(
                        lifetime, 
                        new InjectionConstructor("Default Logger", TraceEventType.Information));

            if (!Container.IsRegistered<IInterceptionBehavior>("TraceBehavior"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IInterceptionBehavior, TraceBehavior>("TraceBehavior", lifetime);

            RegisterSerializers();
            RegisterClientConfigurations();
            RegisterRestClients();
            RegisterRepositories();
        }

        private void RegisterSerializers()
        {
            if (!Container.IsRegistered<ISerializer>("XmlDataSerializer"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ISerializer, XmlDataSerializer>("XmlDataSerializer", lifetime);

            if (!Container.IsRegistered<ISerializer>("JsonSerializer"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ISerializer, JsonSerializer>("JsonSerializer", lifetime);

            if (!Container.IsRegistered<ISerializer>("JsonSerializerWithInterceptor"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ISerializer, JsonSerializer>(
                        "JsonSerializer",
                        lifetime,
                        new Interceptor(new InterfaceInterceptor()),
                        new InterceptionBehavior(Container.Resolve<IInterceptionBehavior>("TraceBehavior")));

            if (!Container.IsRegistered<ISerializer>("DataSerializer"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<ISerializer, DataSerializer>("DataSerializer", lifetime);
        }

        private void RegisterClientConfigurations()
        {
            if (!Container.IsRegistered<IRestClientConfiguration>("Eventful"))
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IRestClientConfiguration, EventfulRestClientConfiguration>("Eventful", lifetime);
        }

        private void RegisterRestClients()
        {
            if (!Container.IsRegistered<IHttpFactory>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IHttpFactory, HttpFactory>(lifetime);

            if (!Container.IsRegistered<ISyncRestClient>("Eventful"))
                Container.RegisterType<ISyncRestClient, RestClient>(
                    "Eventful",
                    new InjectionConstructor(
                        new ResolvedParameter<IHttpFactory>(),
                        new ResolvedParameter<IRestClientConfiguration>("Eventful")));
        }

        private void RegisterRepositories()
        {
            if (!Container.IsRegistered<IEventfulRepository>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IEventfulRepository, EventfulRepository>(lifetime,
                        new InjectionConstructor(new ResolvedParameter<ISyncRestClient>("Eventful")));

            if (!Container.IsRegistered<IApiObjectFactory>())
                using (var lifetime = new HierarchicalLifetimeManager())
                    Container.RegisterType<IApiObjectFactory, ApiObjectFactory>(lifetime);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace wslyvh.Core.Extensions.Policy
{
    public class LazyResolveBuildPlanPolicy : IBuildPlanPolicy
    {
        public void BuildUp(IBuilderContext context)
        {
            if (context.Existing == null)
            {
                var currentContainer = context.NewBuildUp<IUnityContainer>();
                var typeToBuild = GetTypeToBuild(context.BuildKey.Type);
                var nameToBuild = context.BuildKey.Name;

                context.Existing = IsResolvingIEnumerable(typeToBuild) ?
                    CreateResolveAllResolver(currentContainer, typeToBuild) :
                    CreateResolver(currentContainer, typeToBuild, nameToBuild);

                DynamicMethodConstructorStrategy.SetPerBuildSingleton(context);
            }
        }

        private static Type GetTypeToBuild(Type type)
        {
            return type.GetGenericArguments()[0];
        }

        private static bool IsResolvingIEnumerable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }

        private static object CreateResolver(IUnityContainer currentContainer, Type typeToBuild, string nameToBuild)
        {
            var lazyType = typeof(Lazy<>).MakeGenericType(typeToBuild);
            var trampolineType = typeof(ResolveTrampoline<>).MakeGenericType(typeToBuild);
            var delegateType = typeof(Func<>).MakeGenericType(typeToBuild);
            var resolveMethod = trampolineType.GetMethod("Resolve");

            object trampoline = Activator.CreateInstance(trampolineType, currentContainer, nameToBuild);
            object trampolineDelegate = Delegate.CreateDelegate(delegateType, trampoline, resolveMethod);

            return Activator.CreateInstance(lazyType, trampolineDelegate);
        }

        private static object CreateResolveAllResolver(IUnityContainer currentContainer, Type enumerableType)
        {
            var typeToBuild = GetTypeToBuild(enumerableType);
            var lazyType = typeof(Lazy<>).MakeGenericType(enumerableType);
            var trampolineType = typeof(ResolveAllTrampoline<>).MakeGenericType(typeToBuild);
            var delegateType = typeof(Func<>).MakeGenericType(enumerableType);
            var resolveAllMethod = trampolineType.GetMethod("ResolveAll");

            object trampoline = Activator.CreateInstance(trampolineType, currentContainer);
            object trampolineDelegate = Delegate.CreateDelegate(delegateType, trampoline, resolveAllMethod);

            return Activator.CreateInstance(lazyType, trampolineDelegate);
        }

        private class ResolveTrampoline<T>
        {
            private readonly IUnityContainer _container;
            private readonly string _name;

            public ResolveTrampoline(IUnityContainer container, string name)
            {
                _container = container;
                _name = name;
            }

            public T Resolve()
            {
                return _container.Resolve<T>(_name);
            }
        }

        private class ResolveAllTrampoline<T>
        {
            private readonly IUnityContainer _container;

            public ResolveAllTrampoline(IUnityContainer container)
            {
                _container = container;
            }

            public IEnumerable<T> ResolveAll()
            {
                return _container.ResolveAll<T>();
            }
        }
    }
}

using System;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Moq;

namespace wslyvh.Core.Extensions.Builder
{
    public class MoqBuilderStrategy : BuilderStrategy
    {
        private readonly IUnityContainer _container;

        public MoqBuilderStrategy(IUnityContainer container)
        {
            Guard.ArgumentIsNotNull(container, "container");

            _container = container;
        }

        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            var key = context.OriginalBuildKey;

            if (key.Type.IsInterface && !_container.IsRegistered(key.Type, key.Name))
                context.Existing = CreateDynamicMock(key.Type);
        }

        private static object CreateDynamicMock(Type type)
        {
            var genericMockType = typeof(Mock<>).MakeGenericType(type);
            var mock = (Mock)Activator.CreateInstance(genericMockType);
            return mock.Object;
        }
    }
}

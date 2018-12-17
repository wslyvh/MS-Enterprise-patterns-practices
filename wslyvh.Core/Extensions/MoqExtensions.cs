using Microsoft.Practices.Unity;
using Moq;

namespace wslyvh.Core.Extensions
{
    public static class MoqExtensions
    {
        public static Mock<T> RegisterMock<T>(this IUnityContainer container) where T : class
        {
            return RegisterMock<T>(container, string.Empty);
        }

        public static Mock<T> RegisterMock<T>(this IUnityContainer container, string name) where T : class
        {
            var mock = new Mock<T>();

            container.RegisterInstance<Mock<T>>(name, mock);
            container.RegisterInstance<T>(name, mock.Object);

            return mock;
        }

        public static void RegisterExistingMock(this IUnityContainer container, Mock mock)
        {
            RegisterExistingMock(container, string.Empty, mock);
        }

        public static void RegisterExistingMock(this IUnityContainer container, string name, Mock mock) 
        {
            container.RegisterInstance(name, mock);
            container.RegisterInstance(name, mock.Object);
        }

        public static Mock<T> ConfigureMockFor<T>(this IUnityContainer container) where T : class
        {
            return ConfigureMockFor<T>(container, string.Empty);
        }

        public static Mock<T> ConfigureMockFor<T>(this IUnityContainer container, string name) where T : class
        {
            return container.Resolve<Mock<T>>(name);
        }

        public static void VerifyMockFor<T>(this IUnityContainer container) where T : class
        {
            VerifyMockFor<T>(container, string.Empty);
        }

        public static void VerifyMockFor<T>(this IUnityContainer container, string name) where T : class
        {
            container.Resolve<Mock<T>>(name).VerifyAll();
        }
    }
}

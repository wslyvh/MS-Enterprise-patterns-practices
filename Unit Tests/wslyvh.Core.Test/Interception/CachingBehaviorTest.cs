using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Interception;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Test.Mock;
using wslyvh.Core.Web.Caching;

namespace wslyvh.Core.Test.Interception
{
    [TestClass]
    public class CachingBehaviorTest
    {
        private ILogger _logger;
        private ICachingProvider _cachingProvider;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new MemoryStackLogger("MemoryStackLogger", TraceEventType.Information);
            _cachingProvider = new HttpRuntimeCachingProvider();
        }

        [TestMethod]
        public void CachingBehaviorConstructorTest()
        {
            var target = new CachingBehavior(_cachingProvider, _logger);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CachingBehaviorConstructorLoggerNullTest()
        {
            var target = new CachingBehavior(_cachingProvider, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CachingBehaviorConstructorProfilerNullTest()
        {
            var target = new CachingBehavior(null, _logger);
        }

        [TestMethod]
        public void CachingBehaviorInvokeTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ICachingProvider), _cachingProvider);
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<CachingBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            var result = target.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(_cachingProvider.Contains("wslyvh::wslyvh.Core.Test.Mock.TestServiceMock"));
        }

        [TestMethod]
        public void CachingBehaviorDoubleInvokeTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ICachingProvider), _cachingProvider);
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<CachingBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            var result = target.Get();
            var result2 = target.Get();

            Assert.AreEqual(result, result2);
        }

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void CachingBehaviorInvokeWithExceptionTest()
        //{
        //    //Arrange
        //    var container = new UnityContainer();
        //    container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
        //    container.RegisterInstance(typeof(ICachingProvider), _cachingProvider);
        //    container.RegisterInstance(typeof(ILogger), _logger);
        //    container.RegisterType<ITestService, TestServiceMock>(
        //        new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<CachingBehavior>());

        //    //Act
        //    var target = container.Resolve<ITestService>();
        //    target.GetWithException();
        //}

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CachingBehaviorInvokeInputNullTest()
        {
            var target = new CachingBehavior(_cachingProvider, _logger);
            target.Invoke(null, new GetNextInterceptionBehaviorDelegate(() => null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CachingBehaviorInvokeDelegateNullTest()
        {
            var target = new CachingBehavior(_cachingProvider, _logger);
            target.Invoke(new Mock<IMethodInvocation>().Object, null);
        }
    }
}

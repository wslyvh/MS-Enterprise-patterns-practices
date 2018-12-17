using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics;
using wslyvh.Core.Diagnostics;
using wslyvh.Core.Interception;
using wslyvh.Core.Interfaces.Diagnostics;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Interception
{
    [TestClass]
    public class TraceBehaviorTest
    {
        private ILogger _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new MemoryStackLogger("MemoryStackLogger", TraceEventType.Information);
        }

        [TestMethod]
        public void TraceBehaviorConstructorTest()
        {
            var target = new TraceBehavior(_logger);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceBehaviorConstructorNullTest()
        {
            var target = new TraceBehavior(null);
        }

        [TestMethod]
        public void TraceBehaviorInvokeTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<TraceBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            var logger = _logger as MemoryStackLogger;
            var result = target.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, logger.Log.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TraceBehaviorInvokeWithExceptionTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<TraceBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            target.GetWithException();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceBehaviorInvokeInputNullTest()
        {
            var target = new TraceBehavior(_logger);
            target.Invoke(null, new GetNextInterceptionBehaviorDelegate(() => null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceBehaviorInvokeDelegateNullTest()
        {
            var target = new TraceBehavior(_logger);
            target.Invoke(new Mock<IMethodInvocation>().Object, null);
        }
    }
}

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
    public class TraceIdentityBehaviorTest
    {
        private ILogger _logger;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new MemoryStackLogger("MemoryStackLogger", TraceEventType.Information);
        }

        [TestMethod]
        public void TraceIdentityBehaviorConstructorTest()
        {
            var target = new TraceIdentityBehavior(_logger);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceIdentityBehaviorConstructorNullTest()
        {
            var target = new TraceIdentityBehavior(null);
        }

        [TestMethod]
        public void TraceIdentityBehaviorInvokeTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<TraceIdentityBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            var logger = _logger as MemoryStackLogger;
            var result = target.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, logger.Log.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TraceIdentityBehaviorInvokeWithExceptionTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<TraceIdentityBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            target.GetWithException();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceIdentityBehaviorInvokeInputNullTest()
        {
            var target = new TraceIdentityBehavior(_logger);
            target.Invoke(null, new GetNextInterceptionBehaviorDelegate(() => null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TraceIdentityBehaviorInvokeDelegateNullTest()
        {
            var target = new TraceIdentityBehavior(_logger);
            target.Invoke(new Mock<IMethodInvocation>().Object, null);
        }
    }
}

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
    public class ProfilingBehaviorTest
    {
        private ILogger _logger;
        private IProfiler _profiler;

        [TestInitialize]
        public void TestInitialize()
        {
            _logger = new MemoryStackLogger("MemoryStackLogger", TraceEventType.Information);
            _profiler = new Profiler();
        }

        [TestMethod]
        public void ProfilingBehaviorConstructorTest()
        {
            var target = new ProfilingBehavior(_logger, _profiler);

            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProfilingBehaviorConstructorLoggerNullTest()
        {
            var target = new ProfilingBehavior(null, _profiler);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProfilingBehaviorConstructorProfilerNullTest()
        {
            var target = new ProfilingBehavior(_logger, null);
        }

        [TestMethod]
        public void ProfilingBehaviorInvokeTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterInstance(typeof(IProfiler), _profiler);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<ProfilingBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            var logger = _logger as MemoryStackLogger;
            var result = target.Get();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, logger.Log.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void ProfilingBehaviorInvokeWithExceptionTest()
        {
            //Arrange
            var container = new UnityContainer();
            container.AddNewExtension<Microsoft.Practices.Unity.InterceptionExtension.Interception>();
            container.RegisterInstance(typeof(ILogger), _logger);
            container.RegisterInstance(typeof(IProfiler), _profiler);
            container.RegisterType<ITestService, TestServiceMock>(
                new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<ProfilingBehavior>());

            //Act
            var target = container.Resolve<ITestService>();
            target.GetWithException();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProfilingBehaviorInvokeInputNullTest()
        {
            var target = new ProfilingBehavior(_logger, _profiler);
            target.Invoke(null, new GetNextInterceptionBehaviorDelegate(() => null));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProfilingBehaviorInvokeDelegateNullTest()
        {
            var target = new ProfilingBehavior(_logger, _profiler);
            target.Invoke(new Mock<IMethodInvocation>().Object, null);
        }
    }
}

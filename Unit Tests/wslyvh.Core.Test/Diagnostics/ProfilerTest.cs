using wslyvh.Core.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace wslyvh.Core.Test.Diagnostics
{
    [TestClass]
    public class ProfilerTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }
        
        [TestMethod]
        public void ProfilerConstructorTest()
        {
            var profiler = new Profiler();
            Assert.IsNotNull(profiler);
        }

        [TestMethod]
        public void StartTest()
        {
            var profiler = new Profiler();
            profiler.Start();
        }

        [TestMethod]
        public void DoubleStartTest()
        {
            var profiler = new Profiler();
            profiler.Start();
            profiler.Start();
        }

        [TestMethod]
        public void StopTest()
        {
            var target = TimeSpan.Zero;
            var profiler = new Profiler();
            var actual = profiler.Stop();

            Assert.AreEqual(target.TotalMilliseconds, actual.TotalMilliseconds);
        }

        [TestMethod]
        public void StartAndStopTest()
        {
            var actualSeconds = 2;
            var profiler = new Profiler();
            profiler.Start();

            Thread.Sleep(actualSeconds * 1000);
            var actual = profiler.Stop();

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Seconds >= actualSeconds);
        }

        [TestMethod]
        public void DoubleStartAndStopTest()
        {
            var actualSeconds = 2;
            var profiler = new Profiler();
            
            profiler.Start();
            Thread.Sleep(actualSeconds * 1000);
            profiler.Start();

            var actual1 = profiler.Stop();
            var actual2 = profiler.Stop();

            Assert.AreNotSame(actual1.Seconds, actual2.Seconds);
        }
    }
}

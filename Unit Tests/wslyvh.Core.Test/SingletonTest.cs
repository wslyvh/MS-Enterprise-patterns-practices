using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test
{
    [TestClass]
    public class SingletonTest
    {
        [TestMethod]
        public void InstanceTest()
        {
            Assert.AreSame(SingletonMock.Instance, SingletonMock.Instance);
        }

        [TestMethod]
        public void InstanceValueTest()
        {
            Assert.AreEqual(SingletonMock.Instance.Value, SingletonMock.Instance.Value);
        }
    }
}

using System;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interception.Rules;

namespace wslyvh.Core.Test.Interception.Rules
{
    [TestClass]
    public class NotUnityInterceptionAssemblyTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void NotUnityInterceptionAssemblyConstructorTest()
        {
            var target = new NotUnityInterceptionAssembly();
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void MatchesTrueTest()
        {
            var rule = new NotUnityInterceptionAssembly();
            var typeToIntercept = typeof(AssemblyNameStartsWith);
            var typeOfInstance = typeof(AssemblyNameStartsWith);

            var actual = rule.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MatchesFalseTest()
        {
            var rule = new NotUnityInterceptionAssembly();
            var typeToIntercept = typeof(IInterceptionBehavior);
            var typeOfInstance = typeof(IInterceptionBehavior);

            var actual = rule.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(false, actual);
        }
    }
}

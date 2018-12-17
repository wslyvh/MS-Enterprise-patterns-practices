using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interception.Rules;

namespace wslyvh.Core.Test.Interception.Rules
{
    [TestClass]
    public class DoesNotHaveGenericMethodsTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void DoesNotHaveGenericMethodsConstructorTest()
        {
            var target = new DoesNotHaveGenericMethods();
            Assert.IsNotNull(target);
        }

        [TestMethod]
        public void MatchesTrueTest()
        {
            var rule = new DoesNotHaveGenericMethods();
            var typeToIntercept = typeof(UnityContainer);
            var typeOfInstance = typeof(UnityContainer);

            var actual = rule.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MatchesFalseTest()
        {
            var rule = new DoesNotHaveGenericMethods();
            var typeToIntercept = typeof(UnityContainerExtensions);
            var typeOfInstance = typeof(UnityContainerExtensions);

            var actual = rule.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(false, actual);
        }
    }
}

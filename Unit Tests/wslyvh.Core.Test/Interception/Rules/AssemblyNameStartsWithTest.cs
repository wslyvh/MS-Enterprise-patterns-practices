using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interception.Rules;

namespace wslyvh.Core.Test.Interception.Rules
{
    [TestClass]
    public class AssemblyNameStartsWithTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void AssemblyNameStartsWithConstructorTest()
        {
            var target = new AssemblyNameStartsWith("match");
            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AssemblyNameStartsWithConstructorNullTest()
        {
            var target = new AssemblyNameStartsWith(null);
        }

        [TestMethod]
        public void MatchesTrueTest()
        {
            var match = "wslyvh.";
            var target = new AssemblyNameStartsWith(match);
            var typeToIntercept = typeof(AssemblyNameStartsWith);
            var typeOfInstance = typeof(AssemblyNameStartsWith);

            var actual = target.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MatchesFalseTest()
        {
            var match = "wslyvh.";
            var target = new AssemblyNameStartsWith(match);
            var typeToIntercept = typeof(DateTime);
            var typeOfInstance = typeof(DateTime);

            var actual = target.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchesNullTest()
        {
            var target = new AssemblyNameStartsWith(string.Empty);
            target.Matches(null, null);
        }
    }
}

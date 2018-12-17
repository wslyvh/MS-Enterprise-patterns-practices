using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interception.Rules;

namespace wslyvh.Core.Test.Interception.Rules
{
    [TestClass]
    public class AssemblyNameRegexText
    {
        [TestInitialize]
        public void TestInitialize()
        {
        }

        [TestMethod]
        public void AssemblyNameRegexConstructorTest()
        {
            var target = new AssemblyNameRegex(new Regex(string.Empty));
            Assert.IsNotNull(target);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AssemblyNameRegexConstructorNullTest()
        {
            var target = new AssemblyNameRegex(null);
        }

        [TestMethod]
        public void MatchesTrueTest()
        {
            var regex = new Regex("wslyvh.", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            var target = new AssemblyNameRegex(regex);
            var typeToIntercept = typeof(AssemblyNameRegex);
            var typeOfInstance = typeof(AssemblyNameRegex);

            var actual = target.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void MatchesFalseTest()
        {
            var regex = new Regex("wslyvh.", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            var target = new AssemblyNameRegex(regex);
            var typeToIntercept = typeof(DateTime);
            var typeOfInstance = typeof(DateTime);

            var actual = target.Matches(typeToIntercept, typeOfInstance);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MatchesNullTest()
        {
            var target = new AssemblyNameRegex(new Regex(string.Empty));
            target.Matches(null, null);
        }
    }
}

using System;
using System.Text.RegularExpressions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Boot;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Caching
{
    using wslyvh.Core.Boot.Unity;
    using wslyvh.Core.Configuration.Source;

    [TestClass]
    public class HttpRuntimeCachingTest
    {
        private ICachingProvider _cachingProvider;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _cachingProvider = ServiceLocator.Current.GetInstance<ICachingProvider>("HttpRuntimeCachingProvider");
        }

        #region Contains
        [TestMethod]
        public void HttpRuntimeCachingContainsTrueTest()
        {
            var cacheKey = "UnitTests::simple.cache.entity";
            var entity = new SimpleEntityMock()
            {
                Id = 1,
                Name = "Caching Entity"
            };

            _cachingProvider.Insert(cacheKey, entity);

            var actual = _cachingProvider.Contains(cacheKey);

            Assert.AreEqual(true, actual);
            _cachingProvider.Remove(cacheKey);
        }

        [TestMethod]
        public void HttpRuntimeCachingContainsFalseTest()
        {
            var actual = _cachingProvider.Contains("Non-existing CacheKey");

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingContainsNullTest()
        {
            _cachingProvider.Contains(null);
        }
        #endregion

        #region Insert
        [TestMethod]
        public void HttpRuntimeCachingInsertTest()
        {
            var id = new Random().Next(0, 1000);
            var expected = new SimpleEntityMock()
                {
                    Id = id,
                    Name = "Caching Entity"
                };
            var cacheKey = string.Format("UnitTests::simple.cache.entity_{0}", id);
            var actual = _cachingProvider.Insert(cacheKey, expected);

            Assert.AreEqual(expected, actual);
            _cachingProvider.Remove(cacheKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingInsertNullKeyTest()
        {
            _cachingProvider.Insert(null, string.Empty, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingInsertNullValueTest()
        {
            _cachingProvider.Insert<string>(string.Empty, null, TimeSpan.Zero);
        }
        #endregion

        #region Retrieve
        [TestMethod]
        public void HttpRuntimeCachingRetrieveTest()
        {
            var id = new Random().Next(0, 1000);
            var expected = new SimpleEntityMock()
            {
                Id = id,
                Name = "Caching Entity"
            };
            var cacheKey = string.Format("UnitTests::simple.cache.entity_{0}", id);
            _cachingProvider.Insert(cacheKey, expected);

            var actual = _cachingProvider.Retrieve<SimpleEntityMock>(cacheKey);

            Assert.AreSame(expected, actual);

            _cachingProvider.Remove(cacheKey);
        }

        [TestMethod]
        public void HttpRuntimeCachingRetrieveFuncTest()
        {
            var id = new Random().Next(0, 1000);
            var cacheKey = string.Format("UnitTests::simple.cache.entity.Func_{0}", id);
            var actual = _cachingProvider.Retrieve(cacheKey, new TimeSpan(1, 0, 0,0), () => GetSimpleEntity(id));

            Assert.AreSame("Caching Entity from Method", actual.Name);
            _cachingProvider.Remove(cacheKey);
        }

        [TestMethod]
        public void HttpRuntimeCachingRetrieveFuncExistingTest()
        {
            var id = new Random().Next(0, 1000);
            var expected = new SimpleEntityMock()
            {
                Id = id,
                Name = "Caching Entity"
            };
            var cacheKey = string.Format("UnitTests::simple.cache.entity.Func_{0}", id);
            _cachingProvider.Insert(cacheKey, expected);

            var actual = _cachingProvider.Retrieve(cacheKey, new TimeSpan(1, 0, 0, 0), () => GetSimpleEntity(id));

            Assert.AreSame(expected, actual);
            _cachingProvider.Remove(cacheKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingRetrieveNullKeyTest()
        {
            _cachingProvider.Retrieve<SimpleEntityMock>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingRetrieveNullKeyFuncTest()
        {
            _cachingProvider.Retrieve(null, new TimeSpan(1), () => GetSimpleEntity(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingRetrieveNullFuncTest()
        {
            _cachingProvider.Retrieve<SimpleEntityMock>(string.Empty, new TimeSpan(1), null);
        }
        #endregion

        #region Remove
        [TestMethod]
        public void HttpRuntimeCachingRemoveTest()
        {
            var cacheKey = "UnitTests::simple.cache.entity";
            var entity = new SimpleEntityMock { Id = 1, Name = "Caching Entity" };

            _cachingProvider.Insert(cacheKey, entity);
            var actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(true, actual);

            _cachingProvider.Remove(cacheKey);

            actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingRemoveNullTest()
        {
            _cachingProvider.Remove((string)null);
        }

        [TestMethod]
        public void HttpRuntimeCachingRetrieveFlushTest()
        {
            var cacheKey = "UnitTests::simple.cache.entity";
            var entity = new SimpleEntityMock { Id = 1, Name = "Caching Entity" };

            _cachingProvider.Insert(cacheKey, entity);
            var actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(true, actual);

            _cachingProvider.Flush();

            actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void HttpRuntimeCachingRemoveRegexTest()
        {
            var cacheKey = "UnitTests::simple.cache.entity";
            var entity = new SimpleEntityMock { Id = 1, Name = "Caching Entity" };

            _cachingProvider.Insert(cacheKey, entity);
            var actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(true, actual);

            var regex = new Regex("UnitTests::", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            _cachingProvider.Remove(regex);
            actual = _cachingProvider.Contains(cacheKey);

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void HttpRuntimeCachingRemoveRegexNullTest()
        {
            _cachingProvider.Remove((Regex)null);
        }
        #endregion

        #region Private Methods
        private static SimpleEntityMock GetSimpleEntity(int id)
        {
            return new SimpleEntityMock()
            {
                Id = id,
                Name = "Caching Entity from Method"
            };
        }
        #endregion
    }
}

using System;
using System.Text.RegularExpressions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Caching;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Caching
{
    [TestClass]
    public class EntLibCachingTest
    {
        private ICachingProvider _cachingProvider;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _cachingProvider = ServiceLocator.Current.GetInstance<ICachingProvider>("EntLibCachingProvider");
        }

        #region Ctor
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingConstructorNullTest()
        {
            var cachingProvider = new EntLibCachingProvider(null);
        }
        #endregion

        #region Contains
        [TestMethod]
        public void EntLibCachingContainsTrueTest()
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
        public void EntLibCachingContainsFalseTest()
        {
            var actual = _cachingProvider.Contains("Non-existing CacheKey");

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingContainsNullTest()
        {
            _cachingProvider.Contains(null);
        }
        #endregion

        #region Insert
        [TestMethod]
        public void EntLibCachingInsertTest()
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
        public void EntLibCachingInsertNullKeyTest()
        {
            _cachingProvider.Insert(null, string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingInsertNullKeyTimeSpanTest()
        {
            _cachingProvider.Insert(null, string.Empty, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingInsertNullValueTest()
        {
            _cachingProvider.Insert<string>(string.Empty, null, TimeSpan.Zero);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingInsertNullValueTimeSpanTest()
        {
            _cachingProvider.Insert<string>(string.Empty, null);
        }
        #endregion

        #region Retrieve
        [TestMethod]
        public void EntLibCachingRetrieveTest()
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
        public void EntLibCachingRetrieveFuncTest()
        {
            var id = new Random().Next(0, 1000);
            var cacheKey = string.Format("UnitTests::simple.cache.entity.Func_{0}", id);
            var actual = _cachingProvider.Retrieve(cacheKey, new TimeSpan(1, 0, 0,0), () => GetSimpleEntity(id));

            Assert.AreSame("Caching Entity from Method", actual.Name);
            _cachingProvider.Remove(cacheKey);
        }

        [TestMethod]
        public void EntLibCachingRetrieveFuncExistingTest()
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
        public void EntLibCachingRetrieveNullKeyTest()
        {
            _cachingProvider.Retrieve<SimpleEntityMock>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingRetrieveNullKeyFuncTest()
        {
            _cachingProvider.Retrieve(null, new TimeSpan(1), () => GetSimpleEntity(1));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingRetrieveNullFuncTest()
        {
            _cachingProvider.Retrieve<SimpleEntityMock>(string.Empty, new TimeSpan(1), null);
        }
        #endregion

        #region Remove
        [TestMethod]
        public void EntLibCachingRemoveTest()
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

            actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingRemoveNullTest()
        {
            _cachingProvider.Remove((string)null);
        }

        [TestMethod]
        public void EntLibCachingRetrieveFlushTest()
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

            _cachingProvider.Flush();

            actual = _cachingProvider.Contains(cacheKey);
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void EntLibCachingRemoveRegexTest()
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

            var regex = new Regex("UnitTests::", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
            _cachingProvider.Remove(regex);
            actual = _cachingProvider.Contains(cacheKey);

            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EntLibCachingRemoveRegexNullTest()
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

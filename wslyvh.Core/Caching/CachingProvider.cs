using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using wslyvh.Core.Interfaces.Caching;

namespace wslyvh.Core.Caching
{
    public abstract class CachingProvider : ICachingProvider
    {
        private static Dictionary<string, object> _cacheLocks = new Dictionary<string, object>();

        public virtual TimeSpan DefaultAbsoluteExpiration { get; private set; }

        protected CachingProvider()
            : this(new TimeSpan(0, 0, 30, 0, 0))
        {
        }

        protected CachingProvider(TimeSpan defaultAbsoluteExpiration)
        {
            Guard.ArgumentIsNotNull(defaultAbsoluteExpiration, "defaultAbsoluteExpiration");

            DefaultAbsoluteExpiration = defaultAbsoluteExpiration;
        }

        public abstract bool Contains(string key);

        public virtual T Insert<T>(string key, T value) where T : class
        {
            Guard.ArgumentIsNotNull(key, "key");
            Guard.ArgumentIsNotNull(value, "value");

            return Insert(key, value, DefaultAbsoluteExpiration);
        }

        public abstract T Insert<T>(string key, T value, TimeSpan absoluteExpiration) where T : class;

        public abstract T Retrieve<T>(string key) where T : class;

        public virtual T Retrieve<T>(string key, TimeSpan absoluteExpiration, Func<T> objectToCacheRetrievalFunction) where T : class
        {
            Guard.ArgumentIsNotNull(key, "key");
            Guard.ArgumentIsNotNull(objectToCacheRetrievalFunction, "objectToCacheRetrievalFunction");

            var cachedItem = Retrieve<T>(key);
            if (cachedItem != null)
                return cachedItem;

            var cacheLock = GetLockObject(key);
            lock (cacheLock)
            {
                cachedItem = Retrieve<T>(key);
                if (cachedItem != null)
                    return cachedItem;

                var item = objectToCacheRetrievalFunction();

                if (item != null)
                    Insert(key, item, absoluteExpiration);

                return item;
            }
        }

        public abstract void Remove(string key);

        public abstract void Remove(Regex pattern);

        public abstract void Flush();

        private static object GetLockObject(string key)
        {
            if (_cacheLocks.ContainsKey(key))
                return _cacheLocks[key];

            lock (_cacheLocks)
            {
                if (_cacheLocks.ContainsKey(key))
                    return _cacheLocks[key];

                _cacheLocks[key] = new object();
                return _cacheLocks[key];
            }
        }
    }
}

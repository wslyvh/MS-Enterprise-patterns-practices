using System;
using System.Collections;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Caching;

namespace wslyvh.Core.Caching
{
    public class EntLibCachingProvider : CachingProvider
    {
        private readonly ICacheManager _cache;

        public EntLibCachingProvider(ICacheManager cacheManager)
        {
            Guard.ArgumentIsNotNull(cacheManager, "cacheManager");

            _cache = cacheManager;
        }

        public override bool Contains(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");

            return _cache.Contains(key);
        }

        public override T Insert<T>(string key, T value, TimeSpan absoluteExpiration)
        {
            Guard.ArgumentIsNotNull(key, "key");
            Guard.ArgumentIsNotNull(value, "value");

            _cache.Add(key, value);

            return Retrieve<T>(key);
        }

        public override T Retrieve<T>(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");

            return _cache.GetData(key) as T;
        }

        public override void Remove(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");

            _cache.Remove(key);
        }

        public override void Remove(Regex pattern)
        {
            Guard.ArgumentIsNotNull(pattern, "pattern");

            var realCache = _cache.GetType().GetField("realCache", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(_cache) as Cache;

            foreach (DictionaryEntry item in realCache.CurrentCacheState)
            {
                var key = item.Key.ToString();
                if (pattern.IsMatch(key))
                    _cache.Remove(key);
            }
        }

        public override void Flush()
        {
            _cache.Flush();
        }
    }
}

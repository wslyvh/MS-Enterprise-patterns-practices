using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using wslyvh.Core.Caching;

namespace wslyvh.Core.Web.Caching
{
    public class HttpRuntimeCachingProvider : CachingProvider
    {
        private readonly Cache _cache;

        public HttpRuntimeCachingProvider()
        {
            _cache = HttpRuntime.Cache;
        }

        public override bool Contains(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");
            
            return Retrieve<object>(key) != null;
        }

        public override T Insert<T>(string key, T value, TimeSpan absoluteExpiration)
        {
            Guard.ArgumentIsNotNull(key, "key");
            Guard.ArgumentIsNotNull(value, "value");

            _cache.Add(key, value, null, DateTime.Now.Add(absoluteExpiration), TimeSpan.Zero, CacheItemPriority.Default, null);

            return Retrieve<T>(key);
        }

        public override T Retrieve<T>(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");

            return _cache.Get(key) as T;
        }

        public override void Remove(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");

            _cache.Remove(key);
        }

        public override void Remove(Regex pattern)
        {
            Guard.ArgumentIsNotNull(pattern, "pattern");

            var cacheEnum = _cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                if (pattern.IsMatch(cacheEnum.Key.ToString()))
                    _cache.Remove(cacheEnum.Key.ToString());
            }
        }

        public override void Flush()
        {
            var keys = new ArrayList();
            foreach (DictionaryEntry entry in _cache)
            {
                keys.Add(entry.Key);
            }
            foreach (string key in keys)
            {
                _cache.Remove(key);
            }  
        }
    }
}

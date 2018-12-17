using System;
using wslyvh.Core.Interfaces.Caching;
using wslyvh.Core.Interfaces.Configuration;

namespace wslyvh.Core.Configuration
{
    public class CachedSettingProvider : ISettingProvider
    {
        private readonly ISettingProvider _settingProvider;
        private readonly ICachingProvider _cachingProvider;
        private readonly TimeSpan _defaultAbsoluteExpiration;
        private readonly string _baseCacheKey = "::SettingProvider.{0}.{1}-{2}";

        public CachedSettingProvider(ISettingProvider settingProvider, ICachingProvider cachingProvider)
            : this(settingProvider, cachingProvider, new TimeSpan(0, 0, 30, 0, 0))
        {
        }

        public CachedSettingProvider(ISettingProvider settingProvider, ICachingProvider cachingProvider, TimeSpan defaultAbsoluteExpiration)
        {
            Guard.ArgumentIsNotNull(settingProvider, "settingProvider");
            Guard.ArgumentIsNotNull(cachingProvider, "cachingProvider");

            _settingProvider = settingProvider;
            _cachingProvider = cachingProvider;
            _defaultAbsoluteExpiration = defaultAbsoluteExpiration;
        }

        #region ISettingProvider Members
        /// <summary>
        /// Add a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T Add<T>(string key, string value)
        {
            return _settingProvider.Add<T>(key, value);
        }

        /// <summary>
        /// Get the setting <typeparam name="T">value</typeparam> from the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var cacheKey = string.Format(_baseCacheKey, "Add", typeof(T).Name, key);
            return (T) _cachingProvider.Retrieve<object>(cacheKey, _defaultAbsoluteExpiration, () =>
                                                                                               _settingProvider.Get<T>(key));
        }

        /// <summary>
        /// Update a setting <param name="value">value</param> for the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T Update<T>(string key, string value)
        {
            return _settingProvider.Update<T>(key, value);
        }

        /// <summary>
        /// Removes a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Remove(string key)
        {
            _settingProvider.Remove(key);
        }
        #endregion
    }
}

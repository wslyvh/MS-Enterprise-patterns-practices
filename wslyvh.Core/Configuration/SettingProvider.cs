using wslyvh.Core.Interfaces.Configuration;

namespace wslyvh.Core.Configuration
{
    public abstract class SettingProvider : ISettingProvider
    {
        /// <summary>
        /// Add a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract T Add<T>(string key, string value);

        /// <summary>
        /// Get the setting <typeparam name="T">value</typeparam> from the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <returns></returns>
        public abstract T Get<T>(string key);

        /// <summary>
        /// Update a setting <param name="value">value</param> for the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public abstract T Update<T>(string key, string value);

        /// <summary>
        /// Removes a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public abstract void Remove(string key);
    }
}

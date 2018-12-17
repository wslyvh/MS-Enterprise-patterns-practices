
namespace wslyvh.Core.Interfaces.Configuration
{
    public interface ISettingProvider
    {
        /// <summary>
        /// Add a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        T Add<T>(string key, string value);

        /// <summary>
        /// Get the setting <typeparam name="T">value</typeparam> from the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <returns></returns>
        T Get<T>(string key);


        /// <summary>
        /// Update a setting <param name="value">value</param> for the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        T Update<T>(string key, string value);

        /// <summary>
        /// Removes a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);
    }
}

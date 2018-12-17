using System;
using System.Text.RegularExpressions;

namespace wslyvh.Core.Interfaces.Caching
{
    /// <summary>
    /// Interface for a Caching Provider
    /// </summary>
    public interface ICachingProvider
    {
        /// <summary>
        /// Gets the default absolute caching expiration.
        /// </summary>
        /// <value>
        /// The default absolute caching expiration.
        /// </value>
        TimeSpan DefaultAbsoluteExpiration { get; }

        /// <summary>
        /// Determines whether the specified <param name="key">key</param> exists in the cache.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(string key);

        /// <summary>
        /// Inserts <typeparam name="T">T</typeparam> with the specified <param name="key">key</param> to the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        T Insert<T>(string key, T value) where T : class;

        /// <summary>
        /// Inserts <typeparam name="T">T</typeparam> with the specified <param name="key">key</param> and <param name="absoluteExpiration">absolute expiration time</param> to the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        /// <returns></returns>
        T Insert<T>(string key, T value, TimeSpan absoluteExpiration) where T : class;

        /// <summary>
        /// Retrieves <typeparam name="T">T</typeparam> with the specified <param name="key">key</param> from the cache.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Retrieve<T>(string key) where T : class;

        /// <summary>
        /// Retrieves <typeparam name="T">T</typeparam> with the specified <param name="key">key</param> from the cache. If it doesn't exists, inserts it the the specified <param name="objectToCacheRetrievalFunction">func</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="absoluteExpiration">The absolute expiration.</param>
        /// <param name="objectToCacheRetrievalFunction">The object to cache retrieval function.</param>
        /// <returns></returns>
        T Retrieve<T>(string key, TimeSpan absoluteExpiration, Func<T> objectToCacheRetrievalFunction) where T : class;

        /// <summary>
        /// Removes an item from the cache with the specified <param name="key">key</param>.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(string key);

        /// <summary>
        /// Removes items from the cache matching the specified <param name="pattern">pattern</param>.
        /// </summary>
        /// <param name="pattern">The Regex pattern.</param>
        void Remove(Regex pattern);

        /// <summary>
        /// Flushes the cache.
        /// </summary>
        void Flush();
    }
}

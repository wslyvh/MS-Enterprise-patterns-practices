using System;
using System.Configuration;

namespace wslyvh.Core.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        /// <summary>
        /// Add a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override T Add<T>(string key, string value)
        {
            Guard.ArgumentIsNotNull(key, "key");
            Guard.ArgumentIsNotNull(value, "value");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(key, value);
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

            return Get<T>(key);
        }

        /// <summary>
        /// Get the setting <typeparam name="T">value</typeparam> from the specified <param name="key">key</param> and tries to cast <typeparam name="T">T</typeparam>. Else returns default instance of <typeparam name="T">T</typeparam>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">key</exception>
        public override T Get<T>(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");
            var value = ConfigurationManager.AppSettings[key];

            return TryCast<T>(value);
        }

        /// <summary>
        /// Update a setting <param name="value">value</param> for the specified <param name="key">key</param>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override T Update<T>(string key, string value)
        {
            Guard.ArgumentIsNotNull(key, "key");
            Guard.ArgumentIsNotNull(value, "value");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] == null)
                return Add<T>(key, value);

            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

            return Get<T>(key);
        }

        /// <summary>
        /// Removes a setting with the specified <param name="key">key</param>.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Remove(string key)
        {
            Guard.ArgumentIsNotNull(key, "key");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove(key);
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        private static T TryCast<T>(string obj)
        {
            try
            {
                return (T) Convert.ChangeType(obj, typeof (T));
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}

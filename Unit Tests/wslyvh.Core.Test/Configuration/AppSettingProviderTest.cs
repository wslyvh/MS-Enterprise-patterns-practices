using System;
using System.Globalization;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.Configuration;

namespace wslyvh.Core.Test.Configuration
{
    [TestClass]
    public class AppSettingProviderTest
    {
        private ISettingProvider _settingProvider;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _settingProvider = ServiceLocator.Current.GetInstance<ISettingProvider>();
        }

        [TestMethod]
        public void ApplicationSettingsAddTest()
        {
            var expected = "Unit Test Value - Add";
            var actual = _settingProvider.Add<string>("Unit Test Key", expected);

            Assert.AreEqual(expected, actual);
            _settingProvider.Remove("Unit Test Key");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationSettingsAddNullKeyTest()
        {
            _settingProvider.Add<string>(null, "Unit Test Value");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationSettingsAddNullValueTest()
        {
            _settingProvider.Add<string>("Unit Test Key", null);
        }

        [TestMethod]
        public void ApplicationSettingsGetStringTest()
        {
            //Setting needs to be added in App.config
            var expected = "string value";
            var actual = _settingProvider.Get<string>("stringSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationSettingsGetNullTest()
        {
            _settingProvider.Get<string>(null);
        }

        [TestMethod]
        public void ApplicationSettingsGetBoolTest()
        {
            //Setting needs to be added in App.config
            var expected = true;
            var actual = _settingProvider.Get<bool>("boolSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApplicationSettingsGetBoolNegativeTest()
        {
            //Setting needs to be added in App.config
            var expected = false;
            var actual = _settingProvider.Get<bool>("boolIncorrectSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApplicationSettingsGetIntTest()
        {
            //Setting needs to be added in App.config
            var expected = 1;
            var actual = _settingProvider.Get<int>("intSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApplicationSettingsGetIntNegativeTest()
        {
            //Setting needs to be added in App.config
            var expected = 0;
            var actual = _settingProvider.Get<int>("intIncorrectSetting");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ApplicationSettingsUpdateTest()
        {
            _settingProvider.Add<string>("Unit Test Key", "Unit Test Value");

            var expected = "Unit Test Value - Update";
            var actual = _settingProvider.Update<string>("Unit Test Key", expected);

            Assert.AreEqual(expected, actual);
            _settingProvider.Remove("Unit Test Key");
        }

        [TestMethod]
        public void ApplicationSettingsUpdateNonExistingTest()
        {
            var expected = "Unit Test Value - Update";
            var actual = _settingProvider.Update<string>("Unit Test Key", expected);

            Assert.AreEqual(expected, actual);
            _settingProvider.Remove("Unit Test Key");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationSettingsUpdateNullKeyTest()
        {
            _settingProvider.Update<string>("Unit Test Key", null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationSettingsUpdateNullValueTest()
        {
            _settingProvider.Update<string>(null, "Unit Test Value");
        }

        [TestMethod]
        public void ApplicationSettingsUpdateToDifferentTypeTest()
        {
            var expected = 1000;
            var actual = _settingProvider.Update<int>("Unit Test Key", expected.ToString(CultureInfo.InvariantCulture));

            Assert.AreEqual(expected, actual);
            _settingProvider.Remove("Unit Test Key");
        }

        [TestMethod]
        public void ApplicationSettingsRemoveTest()
        {
            _settingProvider.Remove("Unit Test Value - Add");
            _settingProvider.Remove("Unit Test Value - Update");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ApplicationSettingsRemoveNullTest()
        {
            _settingProvider.Remove(null);
        }
    }
}

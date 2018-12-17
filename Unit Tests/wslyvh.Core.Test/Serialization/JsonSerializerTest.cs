using System;
using System.Runtime.Serialization;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Serialization
{
    [TestClass]
    public class JsonSerializerTest
    {
        private ISerializer _serializer;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _serializer = ServiceLocator.Current.GetInstance<ISerializer>("JsonSerializer");
        }

        [TestMethod]
        public void SerializeTest()
        {
            var entity = SimpleEntityMock.CreateDefault();

            var expected = "{\"<Id>k__BackingField\":1,\"<Name>k__BackingField\":\"Test Entity\"}";
            var actual = _serializer.Serialize(entity);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SerializeNullTest()
        {
            _serializer.Serialize<object>(null);
        }

        [TestMethod]
        public void DeserializeTest()
        {
            var expected = SimpleEntityMock.CreateDefault();

            var entity = "{\"<Id>k__BackingField\":1,\"<Name>k__BackingField\":\"Test Entity\"}";
            var actual = _serializer.Deserialize<SimpleEntityMock>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void DeserializeNegativeXmlTest()
        {
            _serializer.Deserialize<SimpleEntityMock>("<InvalidXmlCode>");
        }

        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void DeserializeNegativeTest()
        {
            _serializer.Deserialize<SimpleEntityMock>("<InvalidXmlCode></InvalidXmlCode>");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeNullTest()
        {
            _serializer.Deserialize<object>(null);
        }
    }
}

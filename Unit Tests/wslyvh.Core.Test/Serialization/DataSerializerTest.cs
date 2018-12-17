using System;
using System.Runtime.Serialization;
using System.Xml;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Serialization
{
    [TestClass]
    public class DataSerializerTest
    {
        private ISerializer _serializer;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _serializer = ServiceLocator.Current.GetInstance<ISerializer>("DataSerializer");
        }

        [TestMethod]
        public void SerializeTest()
        {
            var entity = SimpleEntityMock.CreateDefault();

            var expected = @"<SimpleEntityMock xmlns=""http://schemas.datacontract.org/2004/07/wslyvh.Core.Test.Mock"" " +
                @"xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">" +
                "<_x003C_Id_x003E_k__BackingField>1</_x003C_Id_x003E_k__BackingField>" +
                "<_x003C_Name_x003E_k__BackingField>Test Entity</_x003C_Name_x003E_k__BackingField>" +
                "</SimpleEntityMock>";
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

            var entity = @"<SimpleEntityMock xmlns=""http://schemas.datacontract.org/2004/07/wslyvh.Core.Test.Mock"" " +
                @"xmlns:i=""http://www.w3.org/2001/XMLSchema-instance"">" +
                "<_x003C_Id_x003E_k__BackingField>1</_x003C_Id_x003E_k__BackingField>" +
                "<_x003C_Name_x003E_k__BackingField>Test Entity</_x003C_Name_x003E_k__BackingField>" +
                "</SimpleEntityMock>";
            var actual = _serializer.Deserialize<SimpleEntityMock>(entity);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
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

using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wslyvh.Core.Boot.Unity;
using wslyvh.Core.Configuration.Source;
using wslyvh.Core.Interfaces.Serialization;
using wslyvh.Core.Test.Mock;

namespace wslyvh.Core.Test.Serialization
{
    [TestClass]
    public class XmlDataSerializerTest
    {
        private ISerializer _serializer;

        [TestInitialize]
        public void Initialize()
        {
            BootstrapperHelper.StartDefault();

            _serializer = ServiceLocator.Current.GetInstance<ISerializer>("XmlDataSerializer");
        }

        [TestMethod]
        public void SerializeTest()
        {
            var entity = SimpleEntityMock.CreateDefault();

            var expected = @"﻿<?xml version=""1.0"" encoding=""utf-8""?>" +
                           "<SimpleEntityMock>" +
                               "<Id>1</Id>" +
                               "<Name>Test Entity</Name>" +
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

            var entity = @"<SimpleEntityMock>" +
                             "<Id>1</Id>" +
                             "<Name>Test Entity</Name>" +
                         "</SimpleEntityMock>";
            var actual = _serializer.Deserialize<SimpleEntityMock>(entity);
            
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeserializeNegativeTest()
        {
            _serializer.Deserialize<SimpleEntityMock>("<InvalidXmlCode>");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeNullTest()
        {
            _serializer.Deserialize<object>(null);
        }
    }
}

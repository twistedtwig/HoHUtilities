using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CustomConfigurations.Test
{
    [TestFixture]
    public class ConfigurationLoader
    {
        private CustomConfigurations.ConfigurationLoader loader = null;

        [SetUp]
        public void Init()
        {
            loader = (CustomConfigurations.ConfigurationLoader)System.Configuration.ConfigurationManager.GetSection("myCustomGroup/myCustomSection");            
        }

        [Test]
        public void TestThatConfigLoaderFindsValueCollectionAndLoadsItCorrectly()
        {
            ValueItemElementCollection collection = loader.ValueItems;
            Assert.IsNotNull(collection);

            Assert.AreEqual(5,collection.Count);

            ValueItemElement item2 = collection["key2"];
            Assert.IsNotNull(item2);
            Assert.AreEqual("value2", item2.Value);

            ValueItemElement item3 = collection["key3"];
            Assert.IsNotNull(item3);
            Assert.AreEqual("value3", item3.Value);

            ValueItemElement item4 = collection["key4"];
            Assert.IsNotNull(item4);
            Assert.AreEqual("value4", item4.Value);

            ValueItemElement item5 = collection["key5"];
            Assert.IsNotNull(item5);
            Assert.AreEqual("7", item5.Value);

            ValueItemElement item6 = collection["key6"];
            Assert.IsNotNull(item6);
            Assert.AreEqual("0.6", item6.Value);
        }

        [Test]
        public void TestWhenInvalidKeyUsedNullObjectReturnedNoErrorThrown()
        {
            ValueItemElementCollection collection = loader.ValueItems;
            ValueItemElement item = collection["key"];
            Assert.IsNull(item);
        }
    }
}

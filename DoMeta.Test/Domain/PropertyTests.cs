﻿using System;
using DoMeta.Domain.ValueObjects;
using NUnit.Framework;

namespace DoMeta.Test.Domain
{
    [TestFixture]
    public class PropertyTests
    {
        [Test]
        public void CanCreateAndCompareProperties()
        {
            var property1 = new Property("Name", new SystemPropertyType(typeof(string)));
            var property2 = new Property("Name", new SystemPropertyType(typeof(string)));
            var property3 = new Property("Description", new SystemPropertyType(typeof(string)));
            var property4 = new Property("Settings", new MetaPropertyType(Guid.NewGuid()));

            Assert.AreEqual("Name", property1.Name);
            Assert.AreEqual(new SystemPropertyType(typeof(string)), property1.Type);
            Assert.AreEqual(property1, property2);
            Assert.AreNotEqual(property1, property3);
            Assert.AreNotEqual(property1, property4);
        }
    }
}

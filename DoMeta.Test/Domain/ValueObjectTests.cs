using System;
using DoMeta.Domain;
using DoMeta.Domain.ValueObjects;
using NUnit.Framework;

namespace DoMeta.Test.Domain
{
    [TestFixture]
    public class ValueObjectTests
    {
        [Test]
        public void CanRegisterValueObject()
        {
            var boundedContextId = Guid.NewGuid();

            var value = new ValueObject(boundedContextId, "Address");

            Assert.AreNotEqual(Guid.Empty, value.Id);
            Assert.AreEqual(boundedContextId, value.BoundedContextId);
            Assert.AreEqual("Address", value.Name);
            Assert.IsEmpty(value.Properties);
        }

        [Test]
        public void CanAddPropertiesToValueObject()
        {
            var value = new ValueObject(Guid.NewGuid(), "Address");

            value.AddProperty(new Property("Street", new SystemPropertyType(typeof(string))));
            value.AddProperty(new Property("ZipCode", new SystemPropertyType(typeof(int))));

            Assert.AreEqual(2, value.Properties.Count);
        }
    }
}

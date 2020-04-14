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
            var property1 = new Property("Name", new SystemType(typeof(string)));
            var property2 = new Property("Name", new SystemType(typeof(string)));
            var property3 = new Property("Name", new SystemType(typeof(string)), false, true);

            Assert.AreEqual("Name", property1.Name);
            Assert.AreEqual(new SystemType(typeof(string)), property1.Type);
            Assert.AreEqual(property1, property2);
            Assert.AreNotEqual(property1, property3);
        }
    }
}

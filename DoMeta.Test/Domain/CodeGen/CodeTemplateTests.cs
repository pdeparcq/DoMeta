using DoMeta.Domain.CodeGen;
using NUnit.Framework;
using System;

namespace DoMeta.Test.Domain.CodeGen
{
    [TestFixture]
    public class CodeTemplateTests
    {

        [Test]
        public void CanCreateCodeTemplate()
        {
            var template = new CodeTemplate("DomainAggregateRoot", "Entity");

            Assert.AreNotEqual(Guid.Empty, template.Id);
            Assert.AreEqual("DomainAggregateRoot", template.Name);
            Assert.AreEqual("Entity", template.SourceType);
            Assert.IsNull(template.Value);
        }

        [Test]
        public void CanUpdateCodeTemplate()
        {
            var template = new CodeTemplate("DomainAggregateRoot", "Entity");

            template.Update("Hello {{name}}!");

            Assert.AreEqual("DomainAggregateRoot", template.Name);
            Assert.AreEqual("Entity", template.SourceType);
            Assert.AreEqual("Hello {{name}}!", template.Value);
        }
    }
}

using NUnit.Framework;
using System;
using System.Threading.Tasks;
using DoMeta.Domain.CodeGen;
using DoMeta.Application.CodeGen.Commands;
using DoMeta.Application.Meta.Commands;
using DoMeta.Domain.Meta;

namespace DoMeta.Test.Application.CodeGen
{

    [TestFixture]
    public class CodeGenerationTests : IntegrationTestBase
    {

        [Test]
        public async Task CanGenerateCodeForAggregate()
        {
            var template = await Dispatcher.SendAsync<CodeTemplate>(new CreateCodeTemplate
            {
                Name = "DomainAggregateRoot",
                SourceType = "Entity"
            });

            template = await Dispatcher.SendAsync<CodeTemplate>(new UpdateCodeTemplate
            {
                AggregateRootId = template.Id,
                Value = "Hello {{Name}}!"
            });

            var touchpoint = await Dispatcher.SendAsync<Entity>(new RegisterEntity
            {
                BoundedContextId = Guid.NewGuid(),
                Name = "Touchpoint",
                AggregateDomainEventName = "TouchpointCreated"
            });

            var code = await Dispatcher.SendAsync<string>(new GenerateCodeForEntity
            {
                BoundedContextId = touchpoint.BoundedContextId,
                EntityId = touchpoint.Id,
                CodeTemplateId = template.Id
            });

            Assert.AreEqual("Hello Touchpoint!", code);
        }
    }
}

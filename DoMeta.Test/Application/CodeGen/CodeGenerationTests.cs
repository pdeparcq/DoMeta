using DoMeta.Application.Commands;
using DoMeta.Domain.CodeGen.Services;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using DoMeta.Domain.CodeGen;
using DoMeta.Application.CodeGen.Commands;

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
                Name = "Aggregate"
            });

            var touchpoint = await Dispatcher.SendAsync<DoMeta.Domain.Entity>(new RegisterEntity
            {
                BoundedContextId = Guid.NewGuid(),
                Name = "Touchpoint",
                AggregateDomainEventName = "TouchpointCreated"
            });

            var generator = ServiceProvider.GetService<CodeGenerator>();

            var code = await generator.Generate(template.Id, touchpoint);

            Assert.AreEqual("Hello Touchpoint!", code);
        }
    }
}

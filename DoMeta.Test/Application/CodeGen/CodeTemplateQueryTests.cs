using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application.CodeGen.Commands;
using DoMeta.Application.CodeGen.Queries;
using DoMeta.Domain.CodeGen;
using NUnit.Framework;

namespace DoMeta.Test.Application.CodeGen
{
    [TestFixture]
    public class CodeTemplateQueryTests : IntegrationTestBase
    {
        [Test]
        public async Task CanQueryCodeTemplatesByType()
        {
            await Dispatcher.SendAsync<CodeTemplate>(new CreateCodeTemplate
            {
                Name = "DomainAggregateRoot",
                SourceType = "Entity"
            });

            await Dispatcher.SendAsync<CodeTemplate>(new CreateCodeTemplate
            {
                Name = "DomainValueObject",
                SourceType = "ValueObject"
            });

            await Dispatcher.SendAsync<CodeTemplate>(new CreateCodeTemplate
            {
                Name = "InfrastructureDbContext",
                SourceType = "Entity"
            });

            var templates = await Dispatcher.GetResultAsync(new GetCodeTemplatesBySourceType
            {
                SourceType = "Entity"
            });

            Assert.AreEqual(2, templates.Count());

            templates = await Dispatcher.GetResultAsync(new GetCodeTemplatesBySourceType
            {
                SourceType = "ValueObject"
            });

            Assert.AreEqual(1, templates.Count());

            var template = templates.First();

            Assert.AreEqual("DomainValueObject", template.Name);
            Assert.AreEqual("ValueObject", template.SourceType);
        }
    }
}

using DoMeta.Domain.CodeGen;
using DoMeta.Domain.CodeGen.Services;
using DoMeta.Infrastructure.CodeGen;
using Kledex.Domain;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace DoMeta.Test.Domain.CodeGen
{
    [TestFixture]
    public class CodeGeneratorTests
    {
        [Test]
        public async Task CanGenerateCodeFromTemplate()
        {
            // Create mock repository
            var repository = new Mock<IRepository<CodeTemplate>>();
            
            // Create template and assign it a value
            var template = new CodeTemplate("WelcomeTemplate", "Dummy");
            template.Update(@"Welcome {{name}}!");

            repository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).Returns(Task.FromResult(template));

            // Create service using mock repository and handlebars template engine
            var service = new CodeGenerator(repository.Object, new HandlebarsTemplateEngine());

            // Generate the code
            var result = await service.Generate(Guid.NewGuid(), new { name = "Pieter" });

            Assert.AreEqual("Welcome Pieter!", result);
        }
    }
}

using System;
using System.Threading.Tasks;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.CodeGen.Services
{
    public class CodeGenerator
    {
        private readonly IRepository<CodeTemplate> _templateRepository;
        private readonly ITemplateEngine _templateEngine;

        public CodeGenerator(IRepository<CodeTemplate> templateRepository, ITemplateEngine templateEngine)
        {
            Ensure.That(templateRepository).IsNotNull();
            Ensure.That(templateEngine).IsNotNull();

            _templateRepository = templateRepository;
            _templateEngine = templateEngine;
        }

        public async Task<string> Generate(Guid templateId, object data)
        {
            var template = await _templateRepository.GetByIdAsync(templateId);
            var generate = _templateEngine.Compile(template.Value ?? "");
            return generate(data);
        }
    }
}

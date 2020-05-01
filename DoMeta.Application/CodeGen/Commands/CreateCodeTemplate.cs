using DoMeta.Domain.CodeGen;
using Kledex.Domain;

namespace DoMeta.Application.CodeGen.Commands
{
    public class CreateCodeTemplate : DomainCommand<CodeTemplate>
    {
        public string Name { get; set; }
    }
}

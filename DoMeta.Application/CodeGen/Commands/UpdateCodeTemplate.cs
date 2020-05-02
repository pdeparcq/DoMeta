using DoMeta.Domain.CodeGen;
using Kledex.Domain;

namespace DoMeta.Application.CodeGen.Commands
{
    public class UpdateCodeTemplate : DomainCommand<CodeTemplate>
    {
        public string Value { get; set; }
    }
}

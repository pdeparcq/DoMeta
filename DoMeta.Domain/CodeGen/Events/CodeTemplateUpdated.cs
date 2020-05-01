using Kledex.Domain;

namespace DoMeta.Domain.CodeGen.Events
{
    public class CodeTemplateUpdated : DomainEvent
    {
        public string Value { get; set; }
    }
}

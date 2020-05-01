using Kledex.Domain;

namespace DoMeta.Domain.CodeGen.Events
{
    public class CodeTemplateCreated : DomainEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

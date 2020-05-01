using Kledex.Domain;

namespace DoMeta.Domain.CodeGen.Events
{
    public class CodeTemplateUpdated : DomainEvent
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

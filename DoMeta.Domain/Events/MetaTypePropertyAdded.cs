using DoMeta.Domain.ValueObjects;

namespace DoMeta.Domain.Events
{
    public class MetaTypePropertyAdded : Kledex.Domain.DomainEvent
    {
        public Property Property { get; set; }
    }
}

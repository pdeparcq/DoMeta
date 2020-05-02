using DoMeta.Domain.Meta.ValueObjects;

namespace DoMeta.Domain.Meta.Events
{
    public class MetaTypePropertyAdded : Kledex.Domain.DomainEvent
    {
        public Property Property { get; set; }
    }
}

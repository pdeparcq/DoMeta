using DoMeta.Domain.ValueObjects;

namespace DoMeta.Domain.Events
{
    public class EntityPropertyAdded : Kledex.Domain.DomainEvent
    {
        public Property Property { get; set; }
    }
}

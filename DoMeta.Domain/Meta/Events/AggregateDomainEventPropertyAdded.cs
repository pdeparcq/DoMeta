using DoMeta.Domain.Meta.ValueObjects;

namespace DoMeta.Domain.Meta.Events
{
    public class AggregateDomainEventPropertyAdded : Kledex.Domain.DomainEvent
    {
        public string DomainEventName { get; set; }
        public Property Property { get; set; }
    }
}

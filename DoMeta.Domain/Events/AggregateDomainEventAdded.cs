namespace DoMeta.Domain.Events
{
    public class AggregateDomainEventAdded : Kledex.Domain.DomainEvent
    {
        public string Name { get; set; }
    }
}

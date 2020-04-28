using DoMeta.Domain.ValueObjects;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddPropertyToDomainEvent : DomainCommand<Domain.Entity>
    {
        public string DomainEventName { get; set; }
        public Property Property { get; set; }
    }
}

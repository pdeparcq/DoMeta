using DoMeta.Domain.Meta.ValueObjects;
using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands
{
    public class AddPropertyToDomainEvent : DomainCommand<Entity>
    {
        public string DomainEventName { get; set; }
        public Property Property { get; set; }
    }
}

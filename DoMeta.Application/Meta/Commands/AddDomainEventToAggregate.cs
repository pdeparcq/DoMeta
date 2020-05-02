using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands
{
    public class AddDomainEventToAggregate : DomainCommand<Entity>
    {
        public string Name { get; set; }
    }
}

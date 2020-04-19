using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddDomainEventToAggregate : DomainCommand<Domain.Entity>
    {
        public string Name { get; set; }
    }
}

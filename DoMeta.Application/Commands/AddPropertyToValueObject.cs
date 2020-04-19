using DoMeta.Domain.ValueObjects;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddPropertyToValueObject : DomainCommand<Domain.ValueObject>
    {
        public Property Property { get; set; }
    }
}

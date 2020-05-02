using DoMeta.Domain.Meta.ValueObjects;
using Kledex.Domain;
using ValueObject = DoMeta.Domain.Meta.ValueObject;

namespace DoMeta.Application.Meta.Commands
{
    public class AddPropertyToValueObject : DomainCommand<ValueObject>
    {
        public Property Property { get; set; }
    }
}

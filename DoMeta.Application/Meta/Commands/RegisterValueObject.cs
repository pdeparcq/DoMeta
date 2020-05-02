using System;
using Kledex.Domain;
using ValueObject = DoMeta.Domain.Meta.ValueObject;

namespace DoMeta.Application.Meta.Commands
{
    public class RegisterValueObject : DomainCommand<ValueObject>
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
    }
}

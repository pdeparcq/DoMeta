using System;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class RegisterValueObject : DomainCommand<Domain.ValueObject>
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
    }
}

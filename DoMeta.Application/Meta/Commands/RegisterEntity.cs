using System;
using DoMeta.Domain.Meta.ValueObjects;
using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands
{
    public class RegisterEntity : DomainCommand<Entity>
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
        public Property Identity { get; set; }
        public string AggregateDomainEventName { get; set; }
    }
}

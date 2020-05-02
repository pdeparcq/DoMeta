using System;
using Kledex.Domain;

namespace DoMeta.Domain.Meta.Events
{
    public class ValueObjectRegistered : DomainEvent
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
    }
}

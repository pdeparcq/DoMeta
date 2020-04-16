using System;
using Kledex.Domain;

namespace DoMeta.Domain.Events
{
    public class ValueObjectRegistered : DomainEvent
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
    }
}

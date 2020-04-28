using System;
using DoMeta.Domain.ValueObjects;

namespace DoMeta.Domain.Events
{
    public class EntityRegistered : Kledex.Domain.DomainEvent
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
        public Property Identity { get; set; }
    }
}

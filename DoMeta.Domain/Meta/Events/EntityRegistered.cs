using System;
using DoMeta.Domain.Meta.ValueObjects;

namespace DoMeta.Domain.Meta.Events
{
    public class EntityRegistered : Kledex.Domain.DomainEvent
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
        public Property Identity { get; set; }
    }
}

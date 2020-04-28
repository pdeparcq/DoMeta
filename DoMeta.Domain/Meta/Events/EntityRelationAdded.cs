using System;

namespace DoMeta.Domain.Events
{
    public class EntityRelationAdded : Kledex.Domain.DomainEvent
    {
        public string Name { get; set; }
        public Guid MetaTypeId { get; set; }
        public int Minimum { get; set; }
        public int? Maximum { get; set; }
    }
}

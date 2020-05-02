using System;

namespace DoMeta.Infrastructure.Meta.Entities
{
    public class DomainEventProperty
    {
        public virtual DomainEvent DomainEvent { get; set; }
        public Guid DomainEventEntityId { get; set; }
        public string DomainEventName { get; set; }
        public string Name { get; set; }
        public string SystemType { get; set; }
        public virtual MetaType MetaType { get; set; }
        public Guid? MetaTypeId { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace DoMeta.Infrastructure.Entities
{
    public class DomainEvent
    {
        public virtual Entity Parent { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<DomainEventProperty> Properties { get; set; }
    }
}

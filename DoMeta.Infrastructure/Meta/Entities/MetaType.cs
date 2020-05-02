using System;
using System.Collections.Generic;

namespace DoMeta.Infrastructure.Meta.Entities
{
    public class MetaType
    {
        public Guid BoundedContextId { get; set; }
        public Guid MetaTypeId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EntityProperty> Properties { get; set; }
    }
}

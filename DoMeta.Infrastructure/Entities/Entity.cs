using System;
using System.Collections.Generic;

namespace DoMeta.Infrastructure.Entities
{
    public class Entity : MetaType
    {
        public virtual EntityProperty Identity { get; set; }
        public string IdentityPropertyName { get; set; }
        public virtual ICollection<DomainEvent> DomainEvents { get; set; }
        public virtual ICollection<EntityRelation> Relations { get; set; }
    }
}

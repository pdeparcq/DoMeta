using System.Collections.Generic;

namespace DoMeta.Infrastructure.Meta.Entities
{
    public class Entity : MetaType
    {
        public virtual Property Identity { get; set; }
        public string IdentityPropertyName { get; set; }
        public virtual ICollection<DomainEvent> DomainEvents { get; set; }
        public virtual ICollection<EntityRelation> Relations { get; set; }
    }
}

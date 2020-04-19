using System;

namespace DoMeta.Infrastructure.Entities
{
    public class EntityProperty
    {
        public virtual MetaType Parent { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string SystemType { get; set; }
        public virtual MetaType MetaType { get; set; }
        public Guid? MetaTypeId { get; set; }
    }
}

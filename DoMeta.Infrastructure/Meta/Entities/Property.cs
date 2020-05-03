using System;

namespace DoMeta.Infrastructure.Meta.Entities
{
    public class Property
    {
        public virtual MetaType Parent { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public string SystemType { get; set; }
        public virtual MetaType MetaType { get; set; }
        public Guid? MetaTypeId { get; set; }
    }
}

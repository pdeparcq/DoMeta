using System;

namespace DoMeta.Infrastructure.Meta.Entities
{
    public class EntityRelation
    {
        public virtual Entity Parent { get; set; }
        public Guid ParentId { get; set; }
        public string Name { get; set; }
        public virtual MetaType MetaType { get; set; }
        public Guid MetaTypeId { get; set; }
        public int Minimum { get; set; }
        public int? Maximum { get; set; }
    }
}

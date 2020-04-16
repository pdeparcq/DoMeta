using System;

namespace DoMeta.Infrastructure.Entities
{
    public class EntityData
    {
        public Guid BoundedContextId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

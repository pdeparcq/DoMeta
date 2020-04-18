using System;

namespace DoMeta.Api.Models
{
    public class EntityModel
    {
        public Guid Id { get; set; }
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
    }
}

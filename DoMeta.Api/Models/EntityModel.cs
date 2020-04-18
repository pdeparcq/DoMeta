using System;
using System.Collections.Generic;

namespace DoMeta.Api.Models
{
    public class EntityModel
    {
        public Guid Id { get; set; }
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
        public string IdentityPropertyName { get; set; }
        public List<PropertyModel> Properties { get; set; }
    }
}

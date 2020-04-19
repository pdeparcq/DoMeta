using System;
using System.Collections.Generic;

namespace DoMeta.Api.Models.Query
{
    public class ValueObjectModel
    {
        public Guid Id { get; set; }
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
        public List<PropertyModel> Properties { get; set; }
    }
}

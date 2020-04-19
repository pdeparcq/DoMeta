using System.Collections.Generic;

namespace DoMeta.Api.Models
{
    public class DomainEventModel
    {
        public string Name { get; set; }
        public List<PropertyModel> Properties { get; set; }
    }
}

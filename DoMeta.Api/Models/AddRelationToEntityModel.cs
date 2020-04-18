using System;

namespace DoMeta.Api.Models
{
    public class AddRelationToEntityModel
    {
        public string Name { get; set; }
        public Guid MetaTypeId { get; set; }
        public int Minimum { get; set; }
        public int? Maximum { get; set; }
    }
}

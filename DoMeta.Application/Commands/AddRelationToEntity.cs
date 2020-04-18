using System;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddRelationToEntity : DomainCommand<Domain.Entity>
    {
        public string Name { get; set; }
        public Guid MetaTypeId { get; set; }
        public int Minimum { get; set; }
        public int? Maximum { get; set; }
    }
}

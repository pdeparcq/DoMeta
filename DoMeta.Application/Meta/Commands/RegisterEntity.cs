using System;
using DoMeta.Domain.ValueObjects;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class RegisterEntity : DomainCommand<DoMeta.Domain.Entity>
    {
        public Guid BoundedContextId { get; set; }
        public string Name { get; set; }
        public Property Identity { get; set; }
        public string AggregateDomainEventName { get; set; }
    }
}

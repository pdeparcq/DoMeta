using System;
using DoMeta.Domain.ValueObjects;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddPropertyToEntity : DomainCommand<Domain.Entity>
    {
        public Property Property { get; set; }
    }
}

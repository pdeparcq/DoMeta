﻿using DoMeta.Domain.Meta.ValueObjects;
using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands
{
    public class AddPropertyToEntity : DomainCommand<Entity>
    {
        public Property Property { get; set; }
    }
}

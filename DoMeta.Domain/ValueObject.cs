﻿using System;
using DoMeta.Domain.Events;

namespace DoMeta.Domain
{
    public class ValueObject : MetaType
    {
        public ValueObject() { }

        public ValueObject(Guid boundedContextId, string name) : base(boundedContextId, name)
        {
            AddAndApplyEvent(new ValueObjectRegistered
            {
                AggregateRootId = Id,
                BoundedContextId = boundedContextId,
                Name = name
            });
        }

        public void Apply(ValueObjectRegistered @event)
        {
            Id = @event.AggregateRootId;
            BoundedContextId = @event.BoundedContextId;
            Name = @event.Name;
        }
    }
}

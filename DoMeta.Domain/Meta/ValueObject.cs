using System;
using DoMeta.Domain.Meta.Events;

namespace DoMeta.Domain.Meta
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

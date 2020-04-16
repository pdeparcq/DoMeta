using System;
using System.Collections.Generic;
using System.Linq;
using DoMeta.Domain.Events;
using DoMeta.Domain.ValueObjects;
using EnsureThat;
using DomainEvent = DoMeta.Domain.Entities.DomainEvent;

namespace DoMeta.Domain
{
    public class Entity : MetaType
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public Entity(Guid boundedContextId, string name, Property identity = null) 
            : base(boundedContextId, name)
        {
            AddAndApplyEvent(new EntityRegistered
            {
                AggregateRootId = Id,
                BoundedContextId = boundedContextId,
                Name = name,
                Identity = identity ?? new Property("Id", new SystemPropertyType(typeof(Guid)))
            });
        }

        public Property Identity { get; private set; }
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public bool IsAggregateRoot => DomainEvents.Any();

        public void AddDomainEvent(string name)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();

            if(DomainEvents.Any(de => de.Name == name))
                throw new ArgumentException("Domain event with same name already exists for entity", nameof(name));

            AddAndApplyEvent(new EntityDomainEventAdded
            {
                AggregateRootId = Id,
                Name = name
            });
        }

        public DomainEvent GetDomainEvent(string name)
        {
            return _domainEvents.Single(de => de.Name == name);
        }

        public void AddPropertyToDomainEvent(Guid domainEventId, Property property)
        {
            Ensure.That(domainEventId).IsNotDefault();
            Ensure.That(property).IsNotNull();

            AddAndApplyEvent(new EntityDomainEventPropertyAdded
            {
                AggregateRootId = Id,
                DomainEventId = domainEventId,
                Property = property
            });
        }

        public void Apply(EntityRegistered @event)
        {
            Id = @event.AggregateRootId;
            BoundedContextId = @event.BoundedContextId;
            Name = @event.Name;
            Identity = @event.Identity;
        }

        public void Apply(EntityDomainEventAdded @event)
        {
            _domainEvents.Add(new DomainEvent(@event.Name));
        }

        public void Apply(EntityDomainEventPropertyAdded @event)
        {
            var domainEvent = _domainEvents.Single(de => de.Id == @event.DomainEventId);

            domainEvent.AddProperty(@event.Property);
        }
    }
}

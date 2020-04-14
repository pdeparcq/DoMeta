using System;
using System.Collections.Generic;
using System.Linq;
using DoMeta.Domain.Events;
using DoMeta.Domain.ValueObjects;
using EnsureThat;
using Kledex.Domain;
using DomainEvent = DoMeta.Domain.Entities.DomainEvent;

namespace DoMeta.Domain
{
    public class Entity : AggregateRoot
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        private readonly List<Property> _properties = new List<Property>();

        public Entity(Guid boundedContextId, string name, Property identity = null)
        {
            Ensure.That(boundedContextId).IsNotDefault();
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            
            AddAndApplyEvent(new EntityRegistered
            {
                AggregateRootId = Id,
                BoundedContextId = boundedContextId,
                Name = name,
                Identity = identity ?? new Property("Id", new SystemType(typeof(Guid)))
            });
        }

        public Guid BoundedContextId { get; private set; }
        public string Name { get; private set; }
        public Property Identity { get; private set; }
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();
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

        public void AddProperty(Property property)
        {
            Ensure.That(property).IsNotNull();

            if(Properties.Any(p => p.Name == property.Name))
                throw new ArgumentException("Property with same name already exists for entity");

            AddAndApplyEvent(new EntityPropertyAdded
            {
                AggregateRootId = Id,
                Property = property
            });
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

        public void Apply(EntityPropertyAdded @event)
        {
            _properties.Add(@event.Property);
        }

        public void Apply(EntityDomainEventAdded @event)
        {
            _domainEvents.Add(new DomainEvent(@event.Name));
        }

        public void Apply(EntityDomainEventPropertyAdded @event)
        {
            var domainEvent = GetDomainEvent(@event.DomainEventId);

            domainEvent.AddProperty(@event.Property);
        }

        private DomainEvent GetDomainEvent(Guid id)
        {
            return _domainEvents.Single(de => de.Id == id);
        }
    }
}

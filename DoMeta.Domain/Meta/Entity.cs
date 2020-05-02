using System;
using System.Collections.Generic;
using System.Linq;
using DoMeta.Domain.Meta.Entities;
using DoMeta.Domain.Meta.Events;
using DoMeta.Domain.Meta.ValueObjects;
using EnsureThat;
using DomainEvent = DoMeta.Domain.Meta.Entities.DomainEvent;

namespace DoMeta.Domain.Meta
{
    public class Entity : MetaType
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();
        private readonly List<EntityRelation> _relations = new List<EntityRelation>();

        public Entity() { }

        public Entity(Guid boundedContextId, string name, Property identity = null) 
            : base(boundedContextId, name)
        {
            AddAndApplyEvent(new EntityRegistered
            {
                AggregateRootId = Id,
                BoundedContextId = boundedContextId,
                Name = name,
                Identity = identity ?? new Property("Id", typeof(Guid))
            });
        }

        public Property Identity { get; private set; }
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public IReadOnlyCollection<EntityRelation> Relations => _relations.AsReadOnly();
        public bool IsAggregateRoot => DomainEvents.Any();

        public void AddDomainEvent(string name)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();

            if(DomainEvents.Any(de => de.Name == name))
                throw new ArgumentException("Domain event with same name already exists for entity", nameof(name));

            AddAndApplyEvent(new AggregateDomainEventAdded
            {
                AggregateRootId = Id,
                Name = name
            });
        }

        public void AddRelation(string name, Guid metaTypeId, int minimum, int? maximum = null)
        {
            if(Relations.Any(r => r.Name == name))
                throw new ArgumentException("Relation with same name already exists for entity", nameof(name));

            AddAndApplyEvent(new EntityRelationAdded
            {
                AggregateRootId = Id,
                Name = name,
                MetaTypeId = metaTypeId,
                Minimum = minimum,
                Maximum = maximum
            });
        }

        public DomainEvent GetDomainEvent(string name)
        {
            return _domainEvents.Single(de => de.Name == name);
        }

        public void AddPropertyToDomainEvent(string domainEventName, Property property)
        {
            Ensure.That(domainEventName).IsNotNullOrWhiteSpace();
            Ensure.That(property).IsNotNull();

            AddAndApplyEvent(new AggregateDomainEventPropertyAdded
            {
                AggregateRootId = Id,
                DomainEventName = domainEventName,
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

        public void Apply(AggregateDomainEventAdded @event)
        {
            _domainEvents.Add(new DomainEvent(@event.Name));
        }

        public void Apply(AggregateDomainEventPropertyAdded @event)
        {
            var domainEvent = GetDomainEvent(@event.DomainEventName);

            domainEvent.AddProperty(@event.Property);
        }

        public void Apply(EntityRelationAdded @event)
        {
            _relations.Add(new EntityRelation(@event.Name, @event.MetaTypeId, @event.Minimum, @event.Maximum));
        }
    }
}

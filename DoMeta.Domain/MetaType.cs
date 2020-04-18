using System;
using System.Collections.Generic;
using System.Linq;
using DoMeta.Domain.Events;
using DoMeta.Domain.ValueObjects;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain
{
    public abstract class MetaType : AggregateRoot
    {
        private readonly List<Property> _properties = new List<Property>();

        protected MetaType() { }

        protected MetaType(Guid boundedContextId, string name)
        {
            Ensure.That(boundedContextId).IsNotDefault();
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
        }

        public Guid BoundedContextId { get; protected set; }
        public string Name { get; protected set; }
        public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

        public void AddProperty(Property property)
        {
            Ensure.That(property).IsNotNull();

            if (Properties.Any(p => p.Name == property.Name))
                throw new ArgumentException("Property with same name already exists for entity");

            AddAndApplyEvent(new MetaTypePropertyAdded
            {
                AggregateRootId = Id,
                Property = property
            });
        }

        public void Apply(MetaTypePropertyAdded @event)
        {
            _properties.Add(@event.Property);
        }
    }
}
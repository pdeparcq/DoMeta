using System;
using System.Collections.Generic;
using System.Linq;
using DoMeta.Domain.Meta.ValueObjects;
using EnsureThat;

namespace DoMeta.Domain.Meta.Entities
{
    public class DomainEvent : Kledex.Domain.Entity
    {
        private readonly List<Property> _properties = new List<Property>();

        public DomainEvent() { }

        public DomainEvent(string name)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            
            Name = name;
        }

        public string Name { get; }
        public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

        public void AddProperty(Property property)
        {
            Ensure.That(property).IsNotNull();

            if(_properties.Any(p => p.Name == property.Name))
                throw new ArgumentException("Property with same name already exists for domain event");

            _properties.Add(property);
        }

        public void RemoveProperty(Property property)
        {
            Ensure.That(property).IsNotNull();

            _properties.Remove(property);
        }
    }
}

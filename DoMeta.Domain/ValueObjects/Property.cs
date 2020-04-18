using System;
using System.Collections.Generic;
using EnsureThat;

namespace DoMeta.Domain.ValueObjects
{
    public class Property : Kledex.Domain.ValueObject
    {
        public string Name { get; }
        public PropertyType Type { get; }

        public Property() { }

        public Property(string name, Type systemType)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();

            Name = name;
            Type = new PropertyType(systemType);
        }

        public Property(string name, Guid metaTypeId)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();

            Name = name;
            Type = new PropertyType(metaTypeId);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Type;
        }
    }
}

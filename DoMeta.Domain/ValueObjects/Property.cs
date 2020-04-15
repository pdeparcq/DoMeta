﻿using System.Collections.Generic;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.ValueObjects
{
    public class Property : ValueObject
    {
        public string Name { get; }
        public PropertyType Type { get; }

        public Property(string name, PropertyType type)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            Ensure.That(type).IsNotNull();

            Name = name;
            Type = type;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Type;
        }
    }
}

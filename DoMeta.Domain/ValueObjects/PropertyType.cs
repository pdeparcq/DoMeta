using System;
using System.Collections.Generic;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.ValueObjects
{
    public abstract class PropertyType : ValueObject
    {
    }

    public class SystemPropertyType : PropertyType
    {
        public System.Type Type { get; }

        public SystemPropertyType(System.Type type)
        {
            Ensure.That(type).IsNotNull();

            Type = type;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }

    public class MetaPropertyType : PropertyType
    {
        public Guid TypeId { get; }

        public MetaPropertyType(Guid typeId)
        {
            Ensure.That(typeId).IsNotDefault();

            TypeId = typeId;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return TypeId;
        }
    }
}

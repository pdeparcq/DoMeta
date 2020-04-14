using System.Collections.Generic;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.ValueObjects
{
    public class Property : ValueObject
    {
        public string Name { get; }
        public PropertyType Type { get; }
        public bool IsReadOnly { get; }
        public bool IsCollection { get; }

        public Property(string name, PropertyType type, bool isReadOnly = false, bool isCollection = false)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            Ensure.That(type).IsNotNull();

            Name = name;
            Type = type;
            IsReadOnly = isReadOnly;
            IsCollection = isCollection;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Type;
            yield return IsReadOnly;
            yield return IsCollection;
        }
    }
}

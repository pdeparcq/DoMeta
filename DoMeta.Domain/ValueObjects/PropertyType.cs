using System.Collections.Generic;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.ValueObjects
{
    public abstract class PropertyType : ValueObject
    {
    }

    public class SystemType : PropertyType
    {
        public System.Type Type { get; }

        public SystemType(System.Type type)
        {
            Ensure.That(type).IsNotNull();

            Type = type;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
        }
    }
}

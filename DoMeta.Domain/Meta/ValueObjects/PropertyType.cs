using System;
using System.Collections.Generic;
using EnsureThat;

namespace DoMeta.Domain.Meta.ValueObjects
{
    public class PropertyType : Kledex.Domain.ValueObject
    {
        public PropertyType() { }

        public PropertyType(System.Type systemType)
        {
            Ensure.That(systemType).IsNotNull();

            SystemType = systemType;
        }

        public PropertyType(Guid metaTypeId)
        {
            Ensure.That(metaTypeId).IsNotDefault();

            MetaTypeId = metaTypeId;
        }

        public Type SystemType { get; }
        public Guid? MetaTypeId { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return SystemType;
            yield return MetaTypeId;
        }
    }
}

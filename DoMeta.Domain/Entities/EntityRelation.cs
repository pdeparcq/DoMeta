using System;
using EnsureThat;

namespace DoMeta.Domain.Entities
{
    public class EntityRelation : Kledex.Domain.Entity
    {
        public EntityRelation(string name, Guid metaTypeId, int minimum, int? maximum = null)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            Ensure.That(metaTypeId).IsNotDefault();
            Ensure.That(minimum).IsGte(0);

            if (maximum != null)
            {
                Ensure.That(maximum.Value).IsGte(minimum);
            }

            Name = name;
            MetaTypeId = metaTypeId;
            Minimum = minimum;
            Maximum = maximum;
        }

        public string Name { get; }
        public Guid MetaTypeId { get; }
        public int Minimum { get; }
        public int? Maximum { get; }
    }
}

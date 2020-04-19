using System.Linq;
using DoMeta.Api.Models.Query;
using DoMeta.Infrastructure.Entities;

namespace DoMeta.Api.Models
{
    public static class MappingExtensions
    {
        public static EntityModel ToEntityModel(this Entity e)
        {
            return new EntityModel
            {
                BoundedContextId = e.BoundedContextId,
                Id = e.MetaTypeId,
                Name = e.Name,
                IdentityPropertyName = e.Identity.Name,
                Properties = e.Properties.Select(ToPropertyModel).ToList(),
                Relations = e.Relations.Select(ToEntityRelationModel).ToList(),
                DomainEvents = e.DomainEvents.Select(ToDomainEventModel).ToList()
            };
        }

        public static DomainEventModel ToDomainEventModel(this DomainEvent de)
        {
            return new DomainEventModel
            {
                Name = de.Name,
                Properties = de.Properties.Select(ToPropertyModel).ToList()
            };
        }

        public static EntityRelationModel ToEntityRelationModel(this EntityRelation r)
        {
            return new EntityRelationModel()
            {
                Name = r.Name,
                MetaType = new MetaTypInfoModel
                {
                    Id = r.MetaType.MetaTypeId,
                    Name = r.MetaType.Name
                },
                Minimum = r.Minimum,
                Maximum = r.Maximum
            };
        }

        public static PropertyModel ToPropertyModel(this EntityProperty p)
        {
            return new PropertyModel
            {
                Name = p.Name,
                SystemType = p.SystemType,
                MetaType = p.MetaType != null ? new MetaTypInfoModel
                {
                    Id = p.MetaType.MetaTypeId,
                    Name = p.MetaType.Name
                } : null
            };
        }

        public static PropertyModel ToPropertyModel(this DomainEventProperty p)
        {
            return new PropertyModel
            {
                Name = p.Name,
                SystemType = p.SystemType,
                MetaType = p.MetaType != null ? new MetaTypInfoModel
                {
                    Id = p.MetaType.MetaTypeId,
                    Name = p.MetaType.Name
                } : null
            };
        }
    }
}

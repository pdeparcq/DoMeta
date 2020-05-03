using System.Threading.Tasks;
using DoMeta.Domain.Meta.Events;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.Meta.EventHandlers
{
    public class EntityRegisteredHandler : IEventHandlerAsync<EntityRegistered>
    {
        private readonly MetaDbContext _db;

        public EntityRegisteredHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(EntityRegistered @event)
        {
            var entity = _db.Entities.Add(new Entity
            {
                BoundedContextId = @event.BoundedContextId,
                MetaTypeId = @event.AggregateRootId,
                Name = @event.Name
            }).Entity;

            var property = _db.Properties.Add(new Property
            {
                ParentId = @event.AggregateRootId,
                Name = @event.Identity.Name,
                MetaTypeId = @event.Identity.Type.MetaTypeId,
                SystemType = @event.Identity.Type.SystemType?.FullName
            }).Entity;

            await _db.SaveChangesAsync();

            entity.Identity = property;

            await _db.SaveChangesAsync();
        }
    }
}

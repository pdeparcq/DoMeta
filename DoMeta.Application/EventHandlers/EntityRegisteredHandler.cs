using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.EventHandlers
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
            _db.Entities.Add(new Entity
            {
                BoundedContextId = @event.BoundedContextId,
                MetaTypeId = @event.AggregateRootId,
                Name = @event.Name,
                Identity = new Property
                {
                    ParentId = @event.AggregateRootId,
                    Name = @event.Identity.Name,
                    MetaTypeId = @event.Identity.Type.MetaTypeId,
                    SystemType = @event.Identity.Type.SystemType?.FullName
                }
            });

            _db.SaveChanges();

            await Task.CompletedTask;
        }
    }
}

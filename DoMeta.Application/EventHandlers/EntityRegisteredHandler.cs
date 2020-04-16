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
            _db.Entities.Add(new EntityData
            {
                BoundedContextId = @event.BoundedContextId,
                Id = @event.Id,
                Name = @event.Name
            });

            _db.SaveChanges();

            await Task.CompletedTask;
        }
    }
}

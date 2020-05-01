using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.EventHandlers
{
    public class EntityRelationAddedHandler : IEventHandlerAsync<EntityRelationAdded>
    {
        private readonly MetaDbContext _db;

        public EntityRelationAddedHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(EntityRelationAdded @event)
        {
            _db.EntityRelations.Add(new EntityRelation
            {
                ParentId = @event.AggregateRootId,
                Name = @event.Name,
                MetaTypeId = @event.MetaTypeId,
                Minimum = @event.Minimum,
                Maximum = @event.Maximum
            });

            await _db.SaveChangesAsync();
        }
    }
}

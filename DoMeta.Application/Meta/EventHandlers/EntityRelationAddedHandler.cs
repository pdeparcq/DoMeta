using System.Threading.Tasks;
using DoMeta.Domain.Meta.Events;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.Meta.EventHandlers
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

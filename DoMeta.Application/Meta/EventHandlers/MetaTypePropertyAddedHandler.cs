using System.Threading.Tasks;
using DoMeta.Domain.Meta.Events;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.Meta.EventHandlers
{
    public class MetaTypePropertyAddedHandler : IEventHandlerAsync<MetaTypePropertyAdded>
    {
        private readonly MetaDbContext _db;

        public MetaTypePropertyAddedHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(MetaTypePropertyAdded @event)
        {
            _db.Properties.Add(new Property
            {
                ParentId = @event.AggregateRootId,
                Name = @event.Property.Name,
                MetaTypeId = @event.Property.Type.MetaTypeId,
                SystemType = @event.Property.Type.SystemType?.FullName
            });

            await _db.SaveChangesAsync();
        }
    }
}

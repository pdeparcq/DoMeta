using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Domain.ValueObjects;
using DoMeta.Infrastructure;
using EnsureThat;
using Kledex.Events;
using Property = DoMeta.Infrastructure.Entities.Property;

namespace DoMeta.Application.EventHandlers
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

            _db.SaveChanges();

            await Task.CompletedTask;
        }
    }
}

using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Domain.ValueObjects;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Events;

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
            _db.EntityProperties.Add(new EntityProperty
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

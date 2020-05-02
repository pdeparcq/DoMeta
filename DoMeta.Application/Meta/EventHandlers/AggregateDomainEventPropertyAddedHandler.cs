using System.Threading.Tasks;
using DoMeta.Domain.Meta.Events;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.Meta.EventHandlers
{
    public class AggregateDomainEventPropertyAddedHandler : IEventHandlerAsync<AggregateDomainEventPropertyAdded>
    {
        private readonly MetaDbContext _db;

        public AggregateDomainEventPropertyAddedHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(AggregateDomainEventPropertyAdded @event)
        {
            _db.DomainEventProperties.Add(new DomainEventProperty
            {
                DomainEventEntityId = @event.AggregateRootId,
                DomainEventName = @event.DomainEventName,
                Name = @event.Property.Name,
                MetaTypeId = @event.Property.Type.MetaTypeId,
                SystemType = @event.Property.Type.SystemType?.FullName
            });

            await _db.SaveChangesAsync();
        }
    }
}

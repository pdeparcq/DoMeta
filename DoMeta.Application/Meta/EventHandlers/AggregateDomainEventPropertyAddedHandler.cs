using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.EventHandlers
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

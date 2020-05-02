using System.Threading.Tasks;
using DoMeta.Domain.Meta.Events;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.Meta.EventHandlers
{
    public class AggregateDomainEventAddedHandler : IEventHandlerAsync<AggregateDomainEventAdded>
    {
        private readonly MetaDbContext _db;

        public AggregateDomainEventAddedHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(AggregateDomainEventAdded @event)
        {
            _db.DomainEvents.Add(new DomainEvent
            {
                ParentId = @event.AggregateRootId,
                Name = @event.Name
            });

            await _db.SaveChangesAsync();
        }
    }
}

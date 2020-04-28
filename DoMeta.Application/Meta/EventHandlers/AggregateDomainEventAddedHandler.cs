using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.EventHandlers
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

            _db.SaveChanges();

            await Task.CompletedTask;
        }
    }
}

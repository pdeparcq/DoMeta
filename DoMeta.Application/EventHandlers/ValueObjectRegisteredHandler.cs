using System.Threading.Tasks;
using DoMeta.Domain.Events;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.EventHandlers
{
    public class ValueObjectRegisteredHandler : IEventHandlerAsync<ValueObjectRegistered>
    {
        private readonly MetaDbContext _db;

        public ValueObjectRegisteredHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(ValueObjectRegistered @event)
        {
            _db.ValueObjects.Add(new ValueObject
            {
                BoundedContextId = @event.BoundedContextId,
                MetaTypeId = @event.AggregateRootId,
                Name = @event.Name
            });

            _db.SaveChanges();

            await Task.CompletedTask;
        }
    }
}

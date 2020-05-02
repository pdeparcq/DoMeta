using System.Threading.Tasks;
using DoMeta.Domain.Meta.Events;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Events;

namespace DoMeta.Application.Meta.EventHandlers
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

            await _db.SaveChangesAsync();
        }
    }
}

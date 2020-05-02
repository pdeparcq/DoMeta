using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands.Handlers
{
    public class AddPropertyToDomainEventHandler : ICommandHandlerAsync<AddPropertyToDomainEvent>
    {
        private readonly IRepository<Entity> _entityRepository;

        public AddPropertyToDomainEventHandler(IRepository<Entity> entityRepository)
        {
            Ensure.That(entityRepository).IsNotNull();

            _entityRepository = entityRepository;
        }

        public async Task<CommandResponse> HandleAsync(AddPropertyToDomainEvent command)
        {
            var entity = _entityRepository.GetById(command.AggregateRootId);

            entity.AddPropertyToDomainEvent(command.DomainEventName, command.Property);

            return await Task.FromResult(new CommandResponse
            {
                Events = entity.Events,
                Result = entity
            });
        }
    }
}

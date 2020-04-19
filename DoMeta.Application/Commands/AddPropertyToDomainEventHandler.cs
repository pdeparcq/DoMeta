using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddPropertyToDomainEventHandler : ICommandHandlerAsync<AddPropertyToDomainEvent>
    {
        private readonly IRepository<Domain.Entity> _entityRepository;

        public AddPropertyToDomainEventHandler(IRepository<Domain.Entity> entityRepository)
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

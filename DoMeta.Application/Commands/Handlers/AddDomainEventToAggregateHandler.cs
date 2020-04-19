using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;

namespace DoMeta.Application.Commands.Handlers
{
    public class AddDomainEventToAggregateHandler : ICommandHandlerAsync<AddDomainEventToAggregate>
    {
        private readonly IRepository<Domain.Entity> _entityRepository;

        public AddDomainEventToAggregateHandler(IRepository<Domain.Entity> entityRepository)
        {
            Ensure.That(entityRepository).IsNotNull();

            _entityRepository = entityRepository;
        }

        public async Task<CommandResponse> HandleAsync(AddDomainEventToAggregate command)
        {
            var entity = _entityRepository.GetById(command.AggregateRootId);

            entity.AddDomainEvent(command.Name);

            return await Task.FromResult(new CommandResponse
            {
                Events = entity.Events,
                Result = entity
            });
        }
    }
}

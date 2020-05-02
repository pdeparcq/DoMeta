using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands.Handlers
{
    public class AddDomainEventToAggregateHandler : ICommandHandlerAsync<AddDomainEventToAggregate>
    {
        private readonly IRepository<Entity> _entityRepository;

        public AddDomainEventToAggregateHandler(IRepository<Entity> entityRepository)
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

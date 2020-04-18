using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;

namespace DoMeta.Application.Commands
{
    public class AddPropertyToEntityHandler : ICommandHandlerAsync<AddPropertyToEntity>
    {
        private readonly IRepository<Domain.Entity> _entityRepository;

        public AddPropertyToEntityHandler(IRepository<Domain.Entity> entityRepository)
        {
            Ensure.That(entityRepository).IsNotNull();

            _entityRepository = entityRepository;
        }

        public async Task<CommandResponse> HandleAsync(AddPropertyToEntity command)
        {
            var entity = _entityRepository.GetById(command.AggregateRootId);

            entity.AddProperty(command.Property);

            return await Task.FromResult(new CommandResponse
            {
                Events = entity.Events,
                Result = entity
            });
        }
    }
}

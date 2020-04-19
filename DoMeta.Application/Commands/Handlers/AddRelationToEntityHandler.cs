using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;

namespace DoMeta.Application.Commands.Handlers
{
    public class AddRelationToEntityHandler : ICommandHandlerAsync<AddRelationToEntity>
    {
        private readonly IRepository<Domain.Entity> _entityRepository;

        public AddRelationToEntityHandler(IRepository<Domain.Entity> entityRepository)
        {
            Ensure.That(entityRepository).IsNotNull();

            _entityRepository = entityRepository;
        }

        public async Task<CommandResponse> HandleAsync(AddRelationToEntity command)
        {
            var entity = _entityRepository.GetById(command.AggregateRootId);

            entity.AddRelation(command.Name, command.MetaTypeId, command.Minimum, command.Maximum);

            return await Task.FromResult(new CommandResponse
            {
                Events = entity.Events,
                Result = entity
            });
        }
    }
}

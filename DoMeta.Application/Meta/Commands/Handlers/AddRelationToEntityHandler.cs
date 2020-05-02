using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;
using Entity = DoMeta.Domain.Meta.Entity;

namespace DoMeta.Application.Meta.Commands.Handlers
{
    public class AddRelationToEntityHandler : ICommandHandlerAsync<AddRelationToEntity>
    {
        private readonly IRepository<Entity> _entityRepository;

        public AddRelationToEntityHandler(IRepository<Entity> entityRepository)
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

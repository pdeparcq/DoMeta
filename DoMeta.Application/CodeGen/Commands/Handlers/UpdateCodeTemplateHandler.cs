using System.Threading.Tasks;
using DoMeta.Domain.CodeGen;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;

namespace DoMeta.Application.CodeGen.Commands.Handlers
{
    public class UpdateCodeTemplateHandler : ICommandHandlerAsync<UpdateCodeTemplate>
    {
        private readonly IRepository<CodeTemplate> _repository;

        public UpdateCodeTemplateHandler(IRepository<CodeTemplate> repository)
        {
            Ensure.That(repository).IsNotNull();

            _repository = repository;
        }

        public async Task<CommandResponse> HandleAsync(UpdateCodeTemplate command)
        {
            var template = await _repository.GetByIdAsync(command.AggregateRootId);

            template.Update(command.Value);

            return new CommandResponse
            {
                Events = template.Events,
                Result = template
            };
        }
    }
}

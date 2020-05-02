using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application.Meta.Queries;
using DoMeta.Domain.CodeGen.Services;
using EnsureThat;
using Kledex;
using Kledex.Commands;

namespace DoMeta.Application.CodeGen.Commands.Handlers
{
    public class GenerateCodeForEntityHandler : ICommandHandlerAsync<GenerateCodeForEntity>
    {
        private readonly IDispatcher _dispatcher;
        private readonly CodeGenerator _generator;

        public GenerateCodeForEntityHandler(IDispatcher dispatcher, CodeGenerator generator)
        {
            Ensure.That(dispatcher).IsNotNull();
            Ensure.That(generator).IsNotNull();

            _dispatcher = dispatcher;
            _generator = generator;
        }

        public async Task<CommandResponse> HandleAsync(GenerateCodeForEntity command)
        {
            // Query the entity
            var entity = (await _dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = command.BoundedContextId
            })).Single(e => e.MetaTypeId == command.EntityId);

            // Generate the code
            return new CommandResponse
            {
                Result = await _generator.Generate(command.CodeTemplateId, entity)
            };
        }
    }
}

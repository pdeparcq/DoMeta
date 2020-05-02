using DoMeta.Domain.CodeGen;
using Kledex.Commands;
using System.Threading.Tasks;

namespace DoMeta.Application.CodeGen.Commands.Handlers
{
    public class CreateCodeTemplateHandler : ICommandHandlerAsync<CreateCodeTemplate>
    {
        public async Task<CommandResponse> HandleAsync(CreateCodeTemplate command)
        {
            var template = new CodeTemplate(command.Name, command.SourceType);

            return await Task.FromResult(new CommandResponse
            {
                Events = template.Events,
                Result = template
            });
        }
    }
}

using DoMeta.Domain.CodeGen;
using Kledex.Commands;
using System.Threading.Tasks;

namespace DoMeta.Application.CodeGen.Commands.Handlers
{
    public class CreateCodeTemplateHandler : ICommandHandlerAsync<CreateCodeTemplate>
    {
        public Task<CommandResponse> HandleAsync(CreateCodeTemplate command)
        {
            var template = new CodeTemplate(command.Name);

            return Task.FromResult(new CommandResponse
            {
                Events = template.Events,
                Result = template
            });
        }
    }
}

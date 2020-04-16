using System.Threading.Tasks;
using DoMeta.Domain;
using Kledex.Commands;

namespace DoMeta.Application.Commands
{
    public class RegisterEntityHandler : ICommandHandlerAsync<RegisterEntity>
    {
        public Task<CommandResponse> HandleAsync(RegisterEntity command)
        {
            var entity = new Entity(command.BoundedContextId, command.Name, command.Identity);

            return Task.FromResult(new CommandResponse
            {
                Events = entity.Events,
                Result = entity
            });
        }
    }
}

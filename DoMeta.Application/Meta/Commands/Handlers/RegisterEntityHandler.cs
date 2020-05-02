using System.Threading.Tasks;
using DoMeta.Domain.Meta;
using Kledex.Commands;

namespace DoMeta.Application.Meta.Commands.Handlers
{
    public class RegisterEntityHandler : ICommandHandlerAsync<RegisterEntity>
    {
        public Task<CommandResponse> HandleAsync(RegisterEntity command)
        {
            var entity = new Entity(command.BoundedContextId, command.Name, command.Identity);

            if (command.AggregateDomainEventName != null)
            {
                entity.AddDomainEvent(command.AggregateDomainEventName);
            }

            return Task.FromResult(new CommandResponse
            {
                Events = entity.Events,
                Result = entity
            });
        }
    }
}

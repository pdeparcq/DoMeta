using System.Threading.Tasks;
using DoMeta.Domain;
using Kledex.Commands;

namespace DoMeta.Application.Commands.Handlers
{
    public class RegisterValueObjectHandler : ICommandHandlerAsync<RegisterValueObject>
    {
        public Task<CommandResponse> HandleAsync(RegisterValueObject command)
        {
            var valueObject = new ValueObject(command.BoundedContextId, command.Name);

            return Task.FromResult(new CommandResponse
            {
                Events = valueObject.Events,
                Result = valueObject
            });
        }
    }
}

using System.Threading.Tasks;
using EnsureThat;
using Kledex.Commands;
using Kledex.Domain;

namespace DoMeta.Application.Commands.Handlers
{
    public class AddPropertyToValueObjectHandler : ICommandHandlerAsync<AddPropertyToValueObject>
    {
        private readonly IRepository<Domain.ValueObject> _valueObjectRepository;

        public AddPropertyToValueObjectHandler(IRepository<Domain.ValueObject> valueObjectRepository)
        {
            Ensure.That(valueObjectRepository).IsNotNull();

            _valueObjectRepository = valueObjectRepository;
        }

        public async Task<CommandResponse> HandleAsync(AddPropertyToValueObject command)
        {
            var valueObject = _valueObjectRepository.GetById(command.AggregateRootId);

            valueObject.AddProperty(command.Property);

            return await Task.FromResult(new CommandResponse
            {
                Events = valueObject.Events,
                Result = valueObject
            });
        }
    }
}

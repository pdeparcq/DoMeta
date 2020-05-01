using DoMeta.Domain.CodeGen.Events;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.CodeGen
{
    public class CodeTemplate : AggregateRoot
    {
        public CodeTemplate() { }

        public CodeTemplate(string name)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();

            AddAndApplyEvent(new CodeTemplateCreated
            {
                AggregateRootId = Id,
                Name = name,
                Value = "Hello {{name}}!"
        });
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        public void Update(string value)
        {
            Ensure.That(value).IsNotEmptyOrWhiteSpace();

            AddAndApplyEvent(new CodeTemplateUpdated()
            {
                Value = value
            });
        }

        public void Apply(CodeTemplateCreated @event)
        {
            Id = @event.AggregateRootId;
            Name = @event.Name;
            Value = @event.Value;
        }

        public void Apply(CodeTemplateUpdated @event)
        {
            Name = @event.Name;
            Value = @event.Value;
        }
    }
}

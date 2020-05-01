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
                Name = name
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
            Id = @event.Id;
            Name = @event.Name;
            Value = "Hello {{name}}!";
        }

        public void Apply(CodeTemplateUpdated @event)
        {
            Value = @event.Value;
        }
    }
}

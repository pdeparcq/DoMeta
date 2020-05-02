using DoMeta.Domain.CodeGen.Events;
using EnsureThat;
using Kledex.Domain;

namespace DoMeta.Domain.CodeGen
{
    public class CodeTemplate : AggregateRoot
    {
        public CodeTemplate() { }

        public CodeTemplate(string name, string sourceType)
        {
            Ensure.That(name).IsNotEmptyOrWhiteSpace();
            Ensure.That(sourceType).IsNotEmptyOrWhiteSpace();

            AddAndApplyEvent(new CodeTemplateCreated
            {
                AggregateRootId = Id,
                Name = name,
                SourceType = sourceType
        });
        }

        public string Name { get; private set; }
        public string SourceType { get; private set; }
        public string Value { get; private set; }

        public void Update(string value)
        {
            Ensure.That(value).IsNotEmptyOrWhiteSpace();

            AddAndApplyEvent(new CodeTemplateUpdated()
            {
                AggregateRootId = Id,
                Value = value
            });
        }

        public void Apply(CodeTemplateCreated @event)
        {
            Id = @event.AggregateRootId;
            Name = @event.Name;
            SourceType = @event.SourceType;
        }

        public void Apply(CodeTemplateUpdated @event)
        {
            Value = @event.Value;
        }
    }
}

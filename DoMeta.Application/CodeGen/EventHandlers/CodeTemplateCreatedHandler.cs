using DoMeta.Domain.CodeGen.Events;
using DoMeta.Infrastructure.CodeGen;
using DoMeta.Infrastructure.CodeGen.Entities;
using EnsureThat;
using Kledex.Events;
using System.Threading.Tasks;

namespace DoMeta.Application.CodeGen.EventHandlers
{
    public class CodeTemplateCreatedHandler : IEventHandlerAsync<CodeTemplateCreated>
    {

        private readonly CodeGenDbContext _db;

        public CodeTemplateCreatedHandler(CodeGenDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(CodeTemplateCreated @event)
        {
            _db.CodeTemplates.Add(new CodeTemplate()
            {
                Id = @event.AggregateRootId,
                Name = @event.Name,
                SourceType = @event.SourceType
            });

            await _db.SaveChangesAsync();
        }
    }
}

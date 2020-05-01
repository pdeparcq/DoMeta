using DoMeta.Domain.CodeGen.Events;
using DoMeta.Infrastructure.CodeGen;
using EnsureThat;
using Kledex.Events;
using System.Linq;
using System.Threading.Tasks;

namespace DoMeta.Application.CodeGen.EventHandlers
{
    public class CodeTemplateUpdatedHandler : IEventHandlerAsync<CodeTemplateUpdated>
    {
        private readonly CodeGenDbContext _db;

        public CodeTemplateUpdatedHandler(CodeGenDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task HandleAsync(CodeTemplateUpdated @event)
        {
            var template = _db.CodeTemplates.Single(t => t.Id == @event.AggregateRootId);

            template.Name = @event.Name;
            template.Value = @event.Value;

            _db.Update(template);
            
            await _db.SaveChangesAsync();
        }
    }
}

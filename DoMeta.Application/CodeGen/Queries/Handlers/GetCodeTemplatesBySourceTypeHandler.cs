using System.Linq;
using System.Threading.Tasks;
using DoMeta.Infrastructure.CodeGen;
using DoMeta.Infrastructure.CodeGen.Entities;
using EnsureThat;
using Kledex.Queries;

namespace DoMeta.Application.CodeGen.Queries.Handlers
{
    public class GetCodeTemplatesBySourceTypeHandler : IQueryHandlerAsync<GetCodeTemplatesBySourceType, IQueryable<CodeTemplate>>
    {
        private readonly CodeGenDbContext _db;

        public GetCodeTemplatesBySourceTypeHandler(CodeGenDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task<IQueryable<CodeTemplate>> HandleAsync(GetCodeTemplatesBySourceType query)
        {
            return await Task.FromResult(_db.CodeTemplates.Where(t => t.SourceType == query.SourceType));
        }
    }
}

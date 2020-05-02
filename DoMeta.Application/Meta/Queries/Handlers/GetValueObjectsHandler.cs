using System.Linq;
using System.Threading.Tasks;
using DoMeta.Infrastructure.Meta;
using DoMeta.Infrastructure.Meta.Entities;
using EnsureThat;
using Kledex.Queries;
using Microsoft.EntityFrameworkCore;

namespace DoMeta.Application.Meta.Queries.Handlers
{
    public class GetValueObjectsHandler : IQueryHandlerAsync<GetValueObjects, IQueryable<ValueObject>>
    {
        private readonly MetaDbContext _db;

        public GetValueObjectsHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task<IQueryable<ValueObject>> HandleAsync(GetValueObjects query)
        {
            return await Task.FromResult(_db.ValueObjects
                .Include(e => e.Properties).ThenInclude(p => p.MetaType)
                .Where(e => e.BoundedContextId == query.BoundedContextId)
            );
        }
    }
}

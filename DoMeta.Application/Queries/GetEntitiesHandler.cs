using System.Linq;
using System.Threading.Tasks;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Queries;

namespace DoMeta.Application.Queries
{
    public class GetEntitiesHandler : IQueryHandlerAsync<GetEntities, IQueryable<EntityData>>
    {
        private readonly MetaDbContext _db;

        public GetEntitiesHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task<IQueryable<EntityData>> HandleAsync(GetEntities query)
        {
            return await Task.FromResult(_db.Entities.Where(e => e.BoundedContextId == query.BoundedContextId));
        }
    }
}

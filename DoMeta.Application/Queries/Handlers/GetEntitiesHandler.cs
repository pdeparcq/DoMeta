﻿using System.Linq;
using System.Threading.Tasks;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex.Queries;
using Microsoft.EntityFrameworkCore;

namespace DoMeta.Application.Queries.Handlers
{
    public class GetEntitiesHandler : IQueryHandlerAsync<GetEntities, IQueryable<Entity>>
    {
        private readonly MetaDbContext _db;

        public GetEntitiesHandler(MetaDbContext db)
        {
            Ensure.That(db).IsNotNull();

            _db = db;
        }

        public async Task<IQueryable<Entity>> HandleAsync(GetEntities query)
        {
            return await Task.FromResult(_db.Entities
                .Include(e => e.Properties).ThenInclude(p => p.MetaType)
                .Include(e => e.DomainEvents).ThenInclude(de => de.Properties).ThenInclude(p => p.MetaType)
                .Include(e => e.Identity).ThenInclude(p => p.MetaType)
                .Include(e => e.Relations).ThenInclude(r => r.MetaType)
                .Where(e => e.BoundedContextId == query.BoundedContextId)
            );
        }
    }
}

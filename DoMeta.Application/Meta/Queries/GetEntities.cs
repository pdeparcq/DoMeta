using System;
using System.Linq;
using DoMeta.Infrastructure.Entities;
using Kledex.Queries;

namespace DoMeta.Application.Queries
{
    public class GetEntities : IQuery<IQueryable<Entity>>
    {
        public Guid BoundedContextId { get; set; }
    }
}

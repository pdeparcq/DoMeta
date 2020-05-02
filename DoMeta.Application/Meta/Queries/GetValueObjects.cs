using System;
using System.Linq;
using DoMeta.Infrastructure.Meta.Entities;
using Kledex.Queries;

namespace DoMeta.Application.Meta.Queries
{
    public class GetValueObjects : IQuery<IQueryable<ValueObject>>
    {
        public Guid BoundedContextId { get; set; }
    }
}

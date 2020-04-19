using System;
using System.Linq;
using DoMeta.Infrastructure.Entities;
using Kledex.Queries;

namespace DoMeta.Application.Queries
{
    public class GetValueObjects : IQuery<IQueryable<ValueObject>>
    {
        public Guid BoundedContextId { get; set; }
    }
}

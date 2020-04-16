using System;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application.Queries;
using DoMeta.Infrastructure.Entities;
using EnsureThat;
using Kledex;

namespace DoMeta.Application
{
    public class EntityQueryService
    {
        private readonly IDispatcher _dispatcher;

        public EntityQueryService(IDispatcher dispatcher)
        {
            Ensure.That(dispatcher).IsNotNull();

            _dispatcher = dispatcher;
        }

        public async Task<IQueryable<EntityData>> GetEntities(Guid boundedContextId)
        {
            return await _dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = boundedContextId
            });
        }
    }
}
